using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boticatio.Cashback.Dominio
{
    public class Revendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Senha { get; set; }

        public IEnumerable<Compra> Compras { get; set; }
    }
}
