using Boticario.Cashback.Interface.Repositorio;
using Boticatio.Cashback.Dominio;
using System.Collections.Generic;
using System.Linq;

namespace Boticatio.Cashback.Repositorio
{
    public class RevendedorRepositorio : RepositorioBase, IRevendedorRepositorio
    {
        public void Add(Revendedor revendedor)
        {
            Context.Revendedores.Add(revendedor);
            Context.SaveChanges();
        }

        public IEnumerable<Revendedor> Listar()
        {
            return Context.Revendedores.OrderByDescending(x => x.Id).AsEnumerable();
        }
    }
}
