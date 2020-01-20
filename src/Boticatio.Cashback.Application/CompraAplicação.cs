using Boticario.Cashback.Interface.Aplicação;
using Boticario.Cashback.Interface.Repositorio;
using Boticatio.Cashback.Dominio;
using Boticatio.Cashback.Utils.Exceptions;
using System;
using System.Collections.Generic;

namespace Boticatio.Cashback.Application
{
    public class CompraAplicação : ICompraAplicação
    {
        public readonly ICompraRepositorio _compraRepositorio;
        public readonly IRevendedorRepositorio _revendedorRepositorio;
        public CompraAplicação(ICompraRepositorio compraRepositorio, IRevendedorRepositorio revendedorRepositorio)
        {
            _compraRepositorio = compraRepositorio;
            _revendedorRepositorio = revendedorRepositorio;
        }

        public Compra Add(Compra compra)
        {
            if (_revendedorRepositorio.ChecarValidaçãoCPF(compra.Revendedor_Id))
            {
                compra.Status_Id = (int)CompraStatusEnum.Aprovado;
            }
            else
            {
                compra.Status_Id = (int)CompraStatusEnum.Validação;
            }
            compra.AplicarCashback();
            _compraRepositorio.Add(compra);

            return compra;
        }

        public Compra Editar(Compra compra)
        {
            if (compra.StatusEmValidação())
            {
                compra.AplicarCashback();
                _compraRepositorio.Editar(compra);
                return compra;
            }
            else
            {
                throw new StatusErrorException("Para editar, o status da compra deve ser: Em validação");
            }
        }
        public IEnumerable<Compra> Listar(int revendedor_Id)
        {
            return _compraRepositorio.Listar(revendedor_Id);
        }
        public void Remover(int id)
        {
            var compra = _compraRepositorio.GetPeloId(id);

            if (compra.StatusEmValidação())
            {
                _compraRepositorio.Remover(compra);
            }
            else
            {
                throw new StatusErrorException("Para deletar, o status da compra deve ser: Em validação");
            }                
        }
        public Compra GetPeloId(int id)
        {
            return _compraRepositorio.GetPeloId(id);
        }
    }
}
