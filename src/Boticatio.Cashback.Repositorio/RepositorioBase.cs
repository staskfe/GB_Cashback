using Boticatio.Cashback.Infraestrutura;

namespace Boticatio.Cashback.Repositorio
{
    public class RepositorioBase
    {
        public CashbackContext Context;

        public RepositorioBase()
        {
            var factory = new CashbackFactoryContext();
            Context = factory.CreateDbContext(new string[0]);

        }
    }
}
