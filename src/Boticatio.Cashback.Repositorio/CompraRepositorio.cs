using Boticario.Cashback.Interface.Repositorio;
using Boticatio.Cashback.Dominio;
using Microsoft.EntityFrameworkCore;
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
        public IEnumerable<Compra> Listar(int revendedor_Id)
        {
            return Context.Compras
                .Include(x => x.Status)
                .Where(x => x.Revendedor_Id == revendedor_Id)
                .OrderByDescending(x => x.Id);
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
