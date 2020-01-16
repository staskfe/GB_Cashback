using System;

namespace Boticatio.Cashback.ViewModels
{
    public class CompraViewModel
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public float Valor { get; set; }
        public DateTime Data { get; set; }

        public int Revendedor_Id { get; set; }
        public RevendedorViewModel Revendedor { get; set; }
    }
}
