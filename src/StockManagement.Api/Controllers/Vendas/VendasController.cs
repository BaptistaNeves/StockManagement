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

        [HttpGet("obter-vendas-anuladas")]
        public async Task<ActionResult<ICollection<VendaDto>>> ObterVendasAnuladas([FromQuery] PaginationParams paginationParams)
        {
            return Ok(await _vendaService.ObterVendasAnuladas(paginationParams));
        }

        [HttpGet("obter-vendas-hoje")]
        public async Task<ActionResult<ICollection<VendaDto>>> ObterVendasDeHoje([FromQuery] PaginationParams paginationParams)
        {
            return Ok(await _vendaService.ObterVendasDeHoje(paginationParams));
        }

        [HttpGet("obter-vendas-por-mes/{mes:int}")]
        public async Task<ActionResult<ICollection<VendaDto>>> ObterVendasPorMes(int mes, [FromQuery] PaginationParams paginationParams)
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

        [HttpPut("anular-venda/{vendaId:guid}")]
        public async Task<ActionResult> Atualizar(Guid vendaId, AnularVendaInputModel anularVenda)
        {
            if (!ModelState.IsValid) return Resposta(ModelState);

            await _vendaService.AnularVenda(vendaId, anularVenda.ProdutoId, anularVenda.Quantidade);

            return Resposta(anularVenda);
        }

        [HttpDelete("excluir-venda/{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var venda = await _vendaService.ConsultarVendaPorId(id);

            if (venda == null) return NotFound();

            await _vendaService.Remover(id);

            return Resposta();
        }
    }
}
