
using Boticatio.Cashback.Dominio;
using System.Collections.Generic;

namespace Boticario.Cashback.Interface.Repositorio
{
    public interface IRevendedorRepositorio
    {
        void Add(Revendedor revendedor);
        IEnumerable<Revendedor> Listar();
        Revendedor GetRevendedorByEmail(string email);
        bool ChecarValidaçãoCPF(int revendedor_Id);
        Revendedor GetRevendedorById(int id);
        bool ValidarEmailDuplicado(string email);
    }
}
