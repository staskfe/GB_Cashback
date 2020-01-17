using Boticario.Cashback.Interface.Repositorio;
using Boticatio.Cashback.Dominio;
using System.Collections.Generic;
using System.Linq;

namespace Boticatio.Cashback.Repositorio
{
    public class CompraRepositorio : RepositorioBase, ICompraRepositorio
    {
        public void Add(Compra compra)
        {
            Context.Compras.Add(compra);
            Context.SaveChanges();
        }

        public void Editar(Compra compra)
        {
            Context.Compras.Update(compra);
            Context.SaveChanges();
        }
        public IEnumerable<Compra> Listar(int revendedor)
        {
            return Context.Compras.Where(x => x.Revendedor_Id == revendedor);
        }

        public void Remover(Compra compra)
        {
            Context.Compras.Remove(compra);
            Context.SaveChanges();
        }
        public Compra GetPeloId(int id)
        {
            return Context.Compras.FirstOrDefault(x => x.Id == id);
        }
    }
}
