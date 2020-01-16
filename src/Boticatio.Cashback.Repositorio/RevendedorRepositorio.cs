using Boticario.Cashback.Interface.Repositorio;
using Boticatio.Cashback.Dominio;

namespace Boticatio.Cashback.Repositorio
{
    public class RevendedorRepositorio : RepositorioBase, IRevendedorRepositorio
    {
        public void Add(Revendedor revendedor)
        {
            Context.Revendedores.Add(revendedor);
            Context.SaveChanges();
        }
    }
}
