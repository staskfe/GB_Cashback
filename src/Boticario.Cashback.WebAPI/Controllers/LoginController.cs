using Boticario.Cashback.WebAPI.ViewModels;
using Boticatio.Cashback.Dominio.Authenticação;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boticario.Cashback.WebAPI.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]LoginViewModel loginViewModel, [FromServices]SigningConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations)
        {
            //Validar usuario

            var result = tokenConfigurations.CreateToken(loginViewModel.Email, signingConfigurations);
            return base.Ok(result);
        }

    }
}
