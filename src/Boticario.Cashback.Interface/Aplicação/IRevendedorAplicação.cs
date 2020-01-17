
using Boticatio.Cashback.Dominio;
using System.Collections.Generic;

namespace Boticario.Cashback.Interface.Aplicação
{
    public interface IRevendedorAplicação
    {
        void Add(Revendedor revendedor);
        IEnumerable<Revendedor> Listar();
    }
}
