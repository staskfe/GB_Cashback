using Boticario.Cashback.Interface.Aplicação;
using Microsoft.AspNetCore.Mvc;

namespace Boticario.Cashback.WebAPI.Controllers
{
    [ApiController]
    [Route("compra")]
    public class CompraController : ControllerBase
    {
        public ICompraAplicação _compraAplicação;
        public CompraController(ICompraAplicação compraAplicação)
        {
            _compraAplicação = compraAplicação;
        }

        [HttpPost]
        public IActionResult Post()
        {
            _compraAplicação.Add();
            return base.Ok();
        }

    }
}
