using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.InputModels.Vendas;
using StockManagement.Application.Interface.Services.Vendas;
using StockManagement.Application.Interfaces.Notification;
using StockManagement.Core.DTOs.Vendas;
using StockManagement.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockManagement.Api.Controllers.Vendas
{
    public class VendasController : MainController
    {
        private readonly IVendaService _vendaService;
        public VendasController(INotificador noticador, 
                                IVendaService vendaService) : base(noticador)
        {
            _vendaService = vendaService;
        }

        [HttpGet("obter-vendas")]
        public async Task<ActionResult<ICollection<VendaDto>>> ObterTodos([FromQuery] PaginationParams paginationParams)
        {
            return Ok(await _vendaService.ObterVendas(paginationParams));
        }

        [HttpGet("obter-vendas-hoje")]
        public async Task<ActionResult<ICollection<VendaDto>>> ObterVendasDeHoje([FromQuery] PaginationParams paginationParams)
        {
            return Ok(await _vendaService.ObterVendasDeHoje(paginationParams));
        }

        [HttpGet("obter-vendas-por-mes/{mes:string}")]
        public async Task<ActionResult<ICollection<VendaDto>>> ObterVendasPorMes(string mes, [FromQuery] PaginationParams paginationParams)
        {
            return Ok(await _vendaService.ObterVendasPorMes(mes, paginationParams));
        }

        [HttpGet("obter-venda-por-id/{id:guid}")]
        public async Task<ActionResult<VendaDto>> ObterPorId(Guid id)
        {
            return Ok(await _vendaService.ObterVendaPorId(id));
        }

        [HttpPost("realizar-venda")]
        public async Task<ActionResult> Adicionar(VendaInputModel vendaModel)
        {
            if (!ModelState.IsValid) return Resposta(ModelState);

            await _vendaService.Adicionar(vendaModel);

            return Resposta(vendaModel);
        }

        [HttpPut("editar-venda/{id:guid}")]
        public async Task<ActionResult> Atualizar(VendaInputModel vendaModel)
        {
            if (!ModelState.IsValid) return Resposta(ModelState);

            await _vendaService.Atualizar(vendaModel);

            return Resposta(vendaModel);
        }

        [HttpDelete("remover-venda/{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var categoria = await _vendaService.ConsultarVendaPorId(id);

            if (categoria == null) return NotFound();

            await _vendaService.Remover(id);

            return Resposta();
        }
    }
}
