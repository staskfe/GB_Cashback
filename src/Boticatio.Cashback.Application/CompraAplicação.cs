﻿using Boticario.Cashback.Interface.Aplicação;
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
    }
}
