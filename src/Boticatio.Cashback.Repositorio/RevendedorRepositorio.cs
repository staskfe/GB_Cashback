using Boticatio.Cashback.Infraestrutura;
using System.Linq;

namespace Boticatio.Cashback.Repositorio
{
    public class RevendedorRepositorio : RepositorioBase
    {
        public void Test()
        {
            var testw = new CashbackFactoryContext();
            

            using (var context = testw.CreateDbContext())
            {
                var test = context.Revendedores.Where(x => x.Id == 1);
            }
        }
    }
}
