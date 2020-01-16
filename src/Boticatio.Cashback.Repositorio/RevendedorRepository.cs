using Boticatio.Cashback.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Boticatio.Cashback.Repositorio
{
    public class RevendedorRepository
    {
        public void Test()
        {
            var testw = new CashbackFactoryContext();
            

            using (var context = testw.CreateDbContext())
            {
                var test = context.Revendedores.Where(x => x.Id == 1);
                // do stuff
            }
        }
    }
}
