using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Boticatio.Cashback.Infraestrutura
{
    public class CashbackFactoryContext : IDesignTimeDbContextFactory<CashbackContext>
    {
        private const string CONNECTIONSTRING = @"data source=localhost; initial catalog=Boticario; persist security info = False;Integrated Security = SSPI;";
        public CashbackContext CreateDbContext(string[] args)
        {
            var construtor = new DbContextOptionsBuilder<CashbackContext>();
            construtor.UseSqlServer(CONNECTIONSTRING);
            return new CashbackContext(construtor.Options);
        }
    }
}
