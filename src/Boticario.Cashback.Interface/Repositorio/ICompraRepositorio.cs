
using Boticatio.Cashback.Dominio;
using System.Collections.Generic;

namespace Boticario.Cashback.Interface.Repositorio
{
    public interface ICompraRepositorio
    {
        public void Add(Compra compra);
        public void Editar(Compra compra);
        public IEnumerable<Compra> Listar(int revendedor);
        public void Remover(Compra compra);
        public Compra GetPeloId(int id);
    }
}
