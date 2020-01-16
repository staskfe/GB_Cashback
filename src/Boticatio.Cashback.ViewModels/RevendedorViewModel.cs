using Boticatio.Cashback.Dominio;
using System.Collections.Generic;

namespace Boticatio.Cashback.ViewModels
{
    public class RevendedorViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Senha { get; set; }

        public IEnumerable<CompraViewModel> Compras { get; set; }
    }
    public static class RevendedorViewModelExtension
    {
        public static Revendedor ToObject(this RevendedorViewModel revendedorViewModel)
        {
            return new Revendedor
            {
                CPF = revendedorViewModel.CPF,
                Email = revendedorViewModel.Email,
                Nome = revendedorViewModel.Nome,
                Id = revendedorViewModel.Id,
                Senha = revendedorViewModel.Senha
            };
        }
    }
}
