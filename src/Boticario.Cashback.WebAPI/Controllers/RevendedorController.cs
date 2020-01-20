﻿using Boticario.Cashback.Interface.Aplicação;
using Boticatio.Cashback.Utils.Exceptions;
using Boticatio.Cashback.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;

namespace Boticario.Cashback.WebAPI.Controllers
{
    [ApiController]
    [Route("revendedor")]
    public class RevendedorController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public IRevendedorAplicação _revendedorAplicação;
        private readonly ILogger<RevendedorController> _logger;
        public RevendedorController(IHttpClientFactory httpClientFactory, IRevendedorAplicação revendedorAplicação, ILogger<RevendedorController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _revendedorAplicação = revendedorAplicação;
            _logger = logger;
        }

        /// <summary>
        /// Não precisa de authorize, é utilizado na tela de login
        /// </summary>
        /// <param name="revendedorViewModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post(RevendedorViewModel revendedorViewModel)
        {
            try
            {
                var revendedor = revendedorViewModel.ToObject();
                _revendedorAplicação.Add(revendedor);

                return base.Ok(revendedor);
            }
            catch (RevendedorDuplicadoException ex)
            {
                _logger.LogError(string.Format("Ja existe um revendedor com o email {0}", revendedorViewModel.Email), ex);
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

        [Authorize("Bearer")]
        [HttpGet]
        [Route("acumulado")]
        public async System.Threading.Tasks.Task<IActionResult> AcumuladoAsync(int id)
        {
            try
            {
                var revendedor = _revendedorAplicação.GetRevendedorById(id);

                var client = _httpClientFactory.CreateClient("boticario");
                var resposta = await client.GetAsync(string.Format("?cpf={0}", revendedor.CPF));
                var responseStream = await resposta.Content.ReadAsStreamAsync();
                var acumulado = await JsonSerializer.DeserializeAsync<AcumuladoViewModel>(responseStream);

                return base.Ok(acumulado);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao buscar o acumulado", ex);
                throw;
            }

        }
    }
}
