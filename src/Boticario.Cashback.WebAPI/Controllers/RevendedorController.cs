using Boticario.Cashback.Interface.Aplicação;
using Boticatio.Cashback.Dominio;
using Boticatio.Cashback.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Boticario.Cashback.WebAPI.Controllers
{
    [ApiController]
    [Route("revendedor")]
    public class RevendedorController : ControllerBase
    {
        public IRevendedorAplicação _revendedorAplicação;
        public RevendedorController(IRevendedorAplicação revendedorAplicação)
        {
            _revendedorAplicação = revendedorAplicação;
        }

        [HttpPost]
        public IActionResult Post(RevendedorViewModel revendedorViewModel)
        {
            var revendedor = revendedorViewModel.ToObject();
            _revendedorAplicação.Add(revendedor);

            return base.Ok(revendedor);
        }

    }
}
