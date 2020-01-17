using Boticatio.Cashback.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public int Status_Id { get; set; }
        public CompraStatusViewModel Status { get; set; }
        public string  StatusDesc { get; set; }
    }
    public static class CompraViewModelExtension
    {
        public static Compra ToObject(this CompraViewModel compraViewModel)
        {
            return new Compra
            {
                Revendedor_Id = compraViewModel.Revendedor_Id,
                Codigo = compraViewModel.Codigo,
                Data = compraViewModel.Data,
                Valor = compraViewModel.Valor,
                Id = compraViewModel.Id
            };
        }
        public static CompraViewModel ToViewModel(this Compra compra)
        {
            return new CompraViewModel
            {
                Codigo = compra.Codigo,
                Data = compra.Data,
                Id = compra.Id,
                Revendedor_Id = compra.Revendedor_Id,
                Valor = compra.Valor,
                Revendedor = compra.Revendedor?.ToViewModel(),
                Status_Id = compra.Status_Id,
                StatusDesc = compra.Status.Descrição,

                Status = compra.Status?.ToViewModel()
            };
        }

        public static IEnumerable<CompraViewModel> ToViewModels(this IEnumerable<Compra> compras)
        {
            return compras.Select(ToViewModel);
        }

    }
}
