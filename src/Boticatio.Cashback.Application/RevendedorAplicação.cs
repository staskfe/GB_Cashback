using Boticario.Cashback.Interface.Aplicação;
using Boticario.Cashback.Interface.Repositorio;
using Boticatio.Cashback.Dominio;

namespace Boticatio.Cashback.Application
{
    public class RevendedorAplicação : IRevendedorAplicação
    {
        public readonly IRevendedorRepositorio _revendedorRepositorio;
        public RevendedorAplicação(IRevendedorRepositorio revendedorRepositorio)
        {
            _revendedorRepositorio = revendedorRepositorio;
        }

        public void Add(Revendedor revendedor)
        {
            _revendedorRepositorio.Add(revendedor);
        }
    }
}
