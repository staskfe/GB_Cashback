using Boticario.Cashback.Interface.Aplicação;
using Boticatio.Cashback.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Boticario.Cashback.WebAPI.Controllers
{
    [ApiController]
    [Route("rev")]
    public class RevendedorController : ControllerBase
    {
        public IRevendedorAplicação _revendedorAplicação;
        public RevendedorController(IRevendedorAplicação revendedorAplicação)
        {
            _revendedorAplicação = revendedorAplicação;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return base.Ok();
        }


        [HttpPost]
        public IActionResult Post()
        {
            var rev = new Revendedor
            {
                Id = 0,
                CPF = "123",
                Email = "Test@Test",
                Nome = "Felipe",
                Senha = "asdasdas"
            };


            _revendedorAplicação.Add(rev);
            return base.Ok();
        }

    }
}
