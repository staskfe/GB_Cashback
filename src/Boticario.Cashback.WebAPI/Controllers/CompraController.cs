using Boticario.Cashback.Interface.Aplicação;
using Boticatio.Cashback.Utils.Exceptions;
using Boticatio.Cashback.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Boticario.Cashback.WebAPI.Controllers
{
    [ApiController]
    [Route("compra")]
    public class CompraController : ControllerBase
    {
        public readonly ICompraAplicação _compraAplicação;
        private readonly ILogger<RevendedorController> _logger;
        public CompraController(ICompraAplicação compraAplicação, ILogger<RevendedorController> logger)
        {
            _compraAplicação = compraAplicação;
            _logger = logger;
        }

        [Authorize("Bearer")]
        [HttpPost]
        public IActionResult Post(CompraViewModel compraViewModel)
        {
            try
            {
                var compra = compraViewModel.ToObject();
                _compraAplicação.Add(compra);
                return base.Ok(compra.ToViewModel());
            }
            catch (CashbackErrorException ex)
            {
                _logger.LogError("Erro ao calcular o cashback", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao criar um revendedor", ex);
                throw;
            }

        }

        [Authorize("Bearer")]
        [HttpGet]
        public IActionResult Get(int revendedor_Id)
        {
            try
            {
                var compras = _compraAplicação.Listar(revendedor_Id).ToViewModels();
                return base.Ok(compras);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao listar as compras", ex);
                throw;
            }

        }

        [Authorize("Bearer")]
        [HttpPut]
        public IActionResult Editar(CompraViewModel compraViewModel)
        {
            try
            {
                var compra = compraViewModel.ToObject();
                _compraAplicação.Editar(compra);

                return base.Ok(compra);
            }
            catch (StatusErrorException ex)
            {
                _logger.LogError("O status é invalido", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao editar compra", ex);
                throw;
            }

        }

        [Authorize("Bearer")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _compraAplicação.Remover(id);
                return base.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao deletar compra", ex);
                throw;
            }

        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("byid")]
        public IActionResult ById(int id)
        {
            try
            {
                var compras = _compraAplicação.GetPeloId(id).ToViewModel();
                return base.Ok(compras);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao buscar compra pelo id", ex);
                throw;
            }

        }
    }
}
