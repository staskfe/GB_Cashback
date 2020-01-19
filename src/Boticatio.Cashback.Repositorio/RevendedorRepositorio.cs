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
        public bool ChecarValidaçãoCPF(int revendedor_Id)
        {
            return Context.Revendedores.Any(x => x.Id == revendedor_Id && x.CPF == "153.509.460-56" || x.CPF == "15350946056");
        }

        public IEnumerable<Revendedor> Listar()
        {
            return Context.Revendedores.OrderByDescending(x => x.Id).AsEnumerable();
        }
        public Revendedor GetRevendedorByEmail(string email)
        {
            return Context.Revendedores.FirstOrDefault(x => x.Email == email);
        }
    }
}
