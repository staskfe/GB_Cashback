
using Boticatio.Cashback.Dominio;
using System.Collections.Generic;

namespace Boticario.Cashback.Interface.Aplicação
{
    public interface ICompraAplicação
    {
        public void Add(Compra compra);
        public void Editar(Compra compra);
        public IEnumerable<Compra> Listar(int revendedor);
        public void Remover(int id);
        Compra GetPeloId(int id);
    }
}
