using Boticario.Cashback.Interface.Aplicação;
using Boticatio.Cashback.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Boticario.Cashback.WebAPI.Controllers
{
    [ApiController]
    [Route("revendedor")]
    public class RevendedorController : ControllerBase
    {
        public IRevendedorAplicação _revendedorAplicação;
        private readonly ILogger<RevendedorController> _logger;
        public RevendedorController(IRevendedorAplicação revendedorAplicação, ILogger<RevendedorController> logger)
        {
            _revendedorAplicação = revendedorAplicação;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post(RevendedorViewModel revendedorViewModel)
        {
            try
            {
                var revendedor = revendedorViewModel.ToObject();
                _revendedorAplicação.Add(revendedor);

                return base.Ok(revendedor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao criar um revendedor", ex);
                throw;
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _revendedorAplicação.Listar();
                return base.Ok(result.ToViewModels());
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao buscar os revendedores", ex);
                throw;
            }
            
        }
    }
}
