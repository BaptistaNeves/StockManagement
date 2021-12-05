using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.InputModels.Catalogo;
using StockManagement.Application.Interface.Services.Catalogo;
using StockManagement.Application.Interfaces.Notification;
using StockManagement.Application.ViewModels.Catalogo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockManagement.Api.Controllers.Catalogo
{
    public class ProdutosController : MainController
    {
        private readonly IProdutoService _produtoService;
        public ProdutosController(INotificador noticador,
                                   IProdutoService produtoService) : base(noticador)
        {
            _produtoService = produtoService;
        }

        [HttpGet("obter-produtos")]
        public async Task<ActionResult<ICollection<ProdutoViewModel>>> ObterTodos()
        {
            return Ok(await _produtoService.ObterTodos());
        }

        [HttpGet("obter-produto-por-id/{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> ObterPorId(Guid id)
        {
            return Ok(await _produtoService.ObterPorId(id));
        }

        [HttpPost("novo-produto")]
        public async Task<ActionResult> Adicionar(ProdutoInputModel produtoModel)
        {
            if (!ModelState.IsValid) return Resposta(ModelState);

            await _produtoService.Adicionar(produtoModel);

            return Resposta(produtoModel);
        }

        [HttpPut("editar-produto/{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, ProdutoInputModel produtoModel)
        {
            if (!ModelState.IsValid) return Resposta(ModelState);

            await _produtoService.Atualizar(produtoModel);

            return Resposta(produtoModel);
        }

        [HttpDelete("remover-produto/{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var categoria = await _produtoService.ObterPorId(id);

            if (categoria == null) return NotFound();

            await _produtoService.Remover(id);

            return Resposta();
        }

    }
}
