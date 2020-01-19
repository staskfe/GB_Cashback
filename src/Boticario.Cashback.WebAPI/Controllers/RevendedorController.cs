using Boticario.Cashback.Interface.Aplicação;
using Boticatio.Cashback.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            var revendedor = revendedorViewModel.ToObject();
            _revendedorAplicação.Add(revendedor);

            return base.Ok(revendedor);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _revendedorAplicação.Listar();
            return base.Ok(result.ToViewModels());
        }
    }
}
