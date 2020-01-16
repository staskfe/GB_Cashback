
using System;

namespace Boticatio.Cashback.Dominio
{
    public class Compra
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public float Valor { get; set; }
        public DateTime Data { get; set; }

        public int RevendedorId { get; set; }
        public Revendedor Revendedor { get; set; }
    }
}
