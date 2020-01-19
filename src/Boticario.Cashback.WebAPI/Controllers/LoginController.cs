using Boticario.Cashback.Interface.Aplicação;
using Boticatio.Cashback.Dominio.Authenticação;
using Boticatio.Cashback.Utils.Exceptions;
using Boticatio.Cashback.ViewModels.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Boticario.Cashback.WebAPI.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        public IRevendedorAplicação _revendedorAplicação;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IRevendedorAplicação revendedorAplicação, ILogger<LoginController> logger)
        {
            _revendedorAplicação = revendedorAplicação;
            _logger = logger;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]LoginViewModel loginViewModel, [FromServices]SigningConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations)
        {
            try
            {
                var usuario = _revendedorAplicação.ValidarLogin(loginViewModel.ToObject());
                var result = tokenConfigurations.CreateToken(usuario, signingConfigurations);
                return base.Ok(result);
            }

            catch (UsuarioNaoEncontradoException ex)
            {
                _logger.LogError("Usuario e/ou senha incorretos", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao logar", ex);
                throw;
            }
        }

    }
}
