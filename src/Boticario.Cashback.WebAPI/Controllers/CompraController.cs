using Boticario.Cashback.Interface.Aplicação;
using Boticatio.Cashback.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace Boticario.Cashback.WebAPI.Controllers
{
    [ApiController]
    [Route("compra")]
    public class CompraController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ICompraAplicação _compraAplicação;
        public CompraController(ICompraAplicação compraAplicação, IHttpClientFactory httpClientFactory)
        {
            _compraAplicação = compraAplicação;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public IActionResult Post(CompraViewModel compraViewModel)
        {
            var compra = compraViewModel.ToObject();
            _compraAplicação.Add(compra);
            return base.Ok(compra.ToViewModel());
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

        [HttpGet]
        [Route("byid")]
        public IActionResult ById(int id)
        {
            var compras = _compraAplicação.GetPeloId(id).ToViewModel();
            return base.Ok(compras);
        }


        [HttpGet]
        [Route("acumulado")]
        public async System.Threading.Tasks.Task<IActionResult> AcumuladoAsync(string cpf)
        {
            var client = _httpClientFactory.CreateClient("boticario");

            var resposta = await client.GetAsync(string.Format("?cpf={0}", cpf));
            var responseStream = await resposta.Content.ReadAsStreamAsync();
            var acumulado = await JsonSerializer.DeserializeAsync<AcumuladoViewModel>(responseStream);

            return base.Ok(acumulado);
        }
    }
}
