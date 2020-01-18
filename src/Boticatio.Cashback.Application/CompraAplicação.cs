using Boticario.Cashback.Interface.Aplicação;
using Boticario.Cashback.Interface.Repositorio;
using Boticatio.Cashback.Dominio;
using System.Collections.Generic;

namespace Boticatio.Cashback.Application
{
    public class CompraAplicação : ICompraAplicação
    {
        public readonly ICompraRepositorio _compraRepositorio;
        public CompraAplicação(ICompraRepositorio compraRepositorio)
        {
            _compraRepositorio = compraRepositorio;
        }

        public void Add(Compra compra)
        {
            compra.Status_Id = (int)CompraStatusEnum.Validação;
            compra.Revendedor_Id = 1;
            _compraRepositorio.Add(compra);
        }

        public void Editar(Compra compra)
        {
            _compraRepositorio.Editar(compra);
        }
        public IEnumerable<Compra> Listar(int revendedor)
        {
            return _compraRepositorio.Listar(revendedor);
        }
        public void Remover(int id)
        {
            var compra = _compraRepositorio.GetPeloId(id);
            _compraRepositorio.Remover(compra);
        }
        public Compra GetPeloId(int id)
        {
            return _compraRepositorio.GetPeloId(id);
        }
    }
}
