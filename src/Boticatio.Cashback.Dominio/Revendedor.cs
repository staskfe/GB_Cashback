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


        public void CriarHash()
        {
            byte[] data = Encoding.ASCII.GetBytes(this.Senha);
            data = new SHA256Managed().ComputeHash(data);
            this.Senha = Encoding.ASCII.GetString(data);
        }
    }
}
