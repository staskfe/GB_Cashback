﻿using Boticario.Cashback.Interface.Aplicação;
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
    public class CompraController : BaseControllerCustom
    {
        public readonly ICompraAplicação _compraAplicação;
        private readonly ILogger<RevendedorController> _logger;
        public CompraController(ICompraAplicação compraAplicação, ILogger<RevendedorController> logger)
        {
            _compraAplicação = compraAplicação;
            _logger = logger;
        }

        /// <summary>
        /// Cria uma nova compra
        /// </summary>
        /// <param name="compraViewModel"></param>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpPost]
        public IActionResult Post(CompraViewModel compraViewModel)
        {
            try
            {
                var compra = compraViewModel.ToObject();
                var result = _compraAplicação.Add(compra);
                return base.Ok(result.ToViewModel());
            }
            catch (CashbackErrorException ex)
            {
                var message = "Erro ao calcular o cashback";
                _logger.LogError(message, ex);
                return InternalErrorCustom(message);

            }
            catch (Exception ex)
            {
                var message = "Erro ao criar um revendedor";
                _logger.LogError(message, ex);
                return InternalErrorCustom(message);

            }

        }

        /// <summary>
        /// Lista as compras de um revendedor
        /// </summary>
        /// <param name="revendedor_Id"></param>
        /// <returns></returns>
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
                var message = "Erro ao listar as compras";
                _logger.LogError(message, ex);
                return InternalErrorCustom(message);

            }

        }

        /// <summary>
        /// Edita uma compra
        /// </summary>
        /// <param name="compraViewModel"></param>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpPut]
        public IActionResult Editar(CompraViewModel compraViewModel)
        {
            try
            {
                var compra = compraViewModel.ToObject();
                var result =_compraAplicação.Editar(compra);

                return base.Ok(result.ToViewModel());
            }
            catch (StatusErrorException ex)
            {
                var message = "O status é invalido";
                _logger.LogError(message, ex);
                return InternalErrorCustom(message);

            }
            catch (Exception ex)
            {
                var message = "Erro ao editar compra";
                _logger.LogError(message, ex);
                return InternalErrorCustom(message);

            }

        }

        /// <summary>
        /// Deleta uma compra
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                var message = "Erro ao deletar compra";
                _logger.LogError(message, ex);
                return InternalErrorCustom(message);

            }

        }

        /// <summary>
        /// Busca uma compra pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                var message = "Erro ao buscar compra pelo id";
                _logger.LogError(message, ex);
                return InternalErrorCustom(message);

            }

        }
    }
}
