using Boticatio.Cashback.Dominio;
using System.Collections.Generic;
using System.Linq;

namespace Boticatio.Cashback.ViewModels
{
    public class CompraStatusViewModel
    {
        public int Id { get; set; }
        public string Descrição { get; set; }

    }
    public static class CompraStatusViewModelExtension
    {
        public static CompraStatusViewModel ToViewModel(this CompraStatus compraStatus)
        {
            return new CompraStatusViewModel
            {
                Id = compraStatus.Id,
                Descrição = compraStatus.Descrição,
            };
        }

        public static IEnumerable<CompraStatusViewModel> ToViewModels(this IEnumerable<CompraStatus> compras)
        {
            return compras.Select(ToViewModel);
        }

    }
}
