using Boticario.Cashback.Interface.Aplicação;
using Boticatio.Cashback.Dominio.Authenticação;
using Boticatio.Cashback.ViewModels.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boticario.Cashback.WebAPI.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        public IRevendedorAplicação _revendedorAplicação;


        public LoginController(IRevendedorAplicação revendedorAplicação)
        {
            _revendedorAplicação = revendedorAplicação;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]LoginViewModel loginViewModel, [FromServices]SigningConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations)
        {

            var usuario = _revendedorAplicação.ValidarLogin(loginViewModel.ToObject());

            var result = tokenConfigurations.CreateToken(usuario, signingConfigurations);
            return base.Ok(result);
        }

    }
}
