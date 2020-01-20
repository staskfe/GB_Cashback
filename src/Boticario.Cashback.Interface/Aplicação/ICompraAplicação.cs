
using Boticatio.Cashback.Dominio;
using System.Collections.Generic;

namespace Boticario.Cashback.Interface.Aplicação
{
    public interface ICompraAplicação
    {
        Compra Add(Compra compra);
        Compra Editar(Compra compra);
        IEnumerable<Compra> Listar(int revendedor_Id);
        void Remover(int id);
        Compra GetPeloId(int id);
    }
}
