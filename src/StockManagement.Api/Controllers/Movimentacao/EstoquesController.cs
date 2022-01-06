using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.InputModels.Movimentacao;
using StockManagement.Application.Interface.Services.Movimentacao;
using StockManagement.Core.DTOs.Movimentacao;
using StockManagement.Core.Interfaces.Notification;
using StockManagement.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockManagement.Api.Controllers.Movimentacao
{
    public class EstoquesController : MainController
    {
        private readonly IEstoqueService _estoqueService;

        public EstoquesController(INotificador noticador, 
                                  IEstoqueService estoqueService) : base(noticador)
        {
            _estoqueService = estoqueService;
        }

        [HttpGet("obter-estoques")]
        public async Task<ActionResult<ICollection<EstoqueDto>>> ObterTodos([FromQuery] PaginationParams paginationParams)
        {
            return Ok(await _estoqueService.ObterProdutosNoEstoques(paginationParams));
        }

        [HttpGet("obter-produtos-em-baixa")]
        public async Task<ActionResult<ICollection<EstoqueDto>>> ObterProdutosEmBaixa([FromQuery] PaginationParams paginationParams)
        {
            return Ok(await _estoqueService.ObterProdutosEmAbaixaNoEstoque(paginationParams));
        }

        [HttpGet("obter-produtos-vazios")]
        public async Task<ActionResult<ICollection<EstoqueDto>>> ObterProdutosVazios([FromQuery] PaginationParams paginationParams)
        {
            return Ok(await _estoqueService.ObterProdutosVaziosNoEstoque(paginationParams));
        }

        [HttpGet("obter-estoque-por-id/{id:guid}")]
        public async Task<ActionResult<EstoqueDto>> ObterPorId(Guid id)
        {
            var estoque = await _estoqueService.ObterPorId(id);
            if (estoque == null) return NotFound();
            return Ok(estoque);
        }

        [HttpPost("entrada-estoque")]
        public async Task<ActionResult> Adicionar(EstoqueInputModel estoqueModel)
        {
            if (!ModelState.IsValid) return Resposta(ModelState);

            await _estoqueService.Adicionar(estoqueModel);

            return Resposta(estoqueModel);
        }

        [HttpPut("editar-estoque/{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, EstoqueInputModel estoqueModel)
        {
            if (!ModelState.IsValid) return Resposta(ModelState);

            await _estoqueService.Atualizar(estoqueModel);

            return Resposta(estoqueModel);
        }

        [HttpDelete("remover-estoque/{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var estoque = await _estoqueService.ObterPorId(id);

            if (estoque == null) return NotFound();

            await _estoqueService.Remover(id);

            return Resposta();
        }
    }
}
