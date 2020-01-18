using Boticatio.Cashback.Dominio;

namespace Boticatio.Cashback.ViewModels.Authentication
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
    public static class LoginViewModelExtension
    {

        public static Revendedor ToObject(this LoginViewModel login)
        {
            return new Revendedor
            {
                Email = login.Email,
                Senha = login.Senha
            };
        }
    }
}
