using Boticario.Cashback.Interface.Aplicação;
using Boticario.Cashback.Interface.Repositorio;

namespace Boticatio.Cashback.Application
{
    public class CompraAplicação : ICompraAplicação
    {
        public readonly ICompraRepositorio _compraRepositorio;
        public CompraAplicação(ICompraRepositorio compraRepositorio)
        {
            _compraRepositorio = compraRepositorio;
        }

        public void Add()
        {
            _compraRepositorio.Add();
        }
    }
}
