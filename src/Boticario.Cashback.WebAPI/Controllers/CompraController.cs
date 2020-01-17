using Boticario.Cashback.Interface.Aplicação;
using Boticatio.Cashback.ViewModels;
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
        public IActionResult Post(CompraViewModel compraViewModel)
        {
            var compra = compraViewModel.ToObject();
            _compraAplicação.Add(compra);

            return base.Ok(compra);
        }
        [HttpGet]
        public IActionResult Get(int revendedor_Id)
        {
            var compras = _compraAplicação.Listar(revendedor_Id).ToViewModels();
            return base.Ok(compras);
        }

        [HttpPut]
        public IActionResult Editar(CompraViewModel compraViewModel)
        {
            var compra = compraViewModel.ToObject();
            _compraAplicação.Editar(compra);

            return base.Ok(compra);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _compraAplicação.Remover(id);
            return base.Ok();
        }

    }
}
