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
    public class LoginController : BaseControllerCustom
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
        public object Post([FromBody]LoginViewModel loginViewModel, [FromServices]SigningConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations)
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
                var error = new { authenticated = false, message = "Usuario e/ou senha incorretos" };
                return base.Ok(error);
            }
            catch (Exception ex)
            {
                var message = "Erro ao logar";
                _logger.LogError(message, ex);
                return InternalErrorCustom(message);
            }
        }

    }
}
