using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

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


        public string CriarHash()
        {
            byte[] data = Encoding.ASCII.GetBytes(this.Senha);
            data = new SHA256Managed().ComputeHash(data);
            return Encoding.ASCII.GetString(data);
        }
    }
}
