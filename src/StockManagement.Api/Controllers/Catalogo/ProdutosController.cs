using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.InputModels.Catalogo;
using StockManagement.Application.Interface.Services.Catalogo;
using StockManagement.Application.Interfaces.Notification;
using StockManagement.Core.DTOs.Catalogo;
using StockManagement.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<ActionResult<ICollection<ProdutoDto>>> ObterTodos([FromQuery] PaginationParams paginationParams)
        {
            return Ok(await _produtoService.ObterTodos(paginationParams));
        }

        [HttpGet("obter-produto-por-id/{id:guid}")]
        public async Task<ActionResult<ProdutoDto>> ObterPorId(Guid id)
        {
            return Ok(await _produtoService.ObterPorId(id));
        }

        [HttpPost("novo-produto")]
        public async Task<ActionResult> Adicionar([FromForm] ProdutoInputModel produtoModel)
        {
            if (!ModelState.IsValid) return Resposta(ModelState);

            var prefixoDaImagem = Guid.NewGuid() + "_";

            if (!await GuardarImagemEnviada(produtoModel.ArquivoEnviado, prefixoDaImagem))
                return Resposta(produtoModel);

            produtoModel.Imagem = prefixoDaImagem + produtoModel.ArquivoEnviado.FileName;

            await _produtoService.Adicionar(produtoModel);

            return Resposta(produtoModel);
        }

        [HttpPut("editar-produto/{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, ProdutoInputModel produtoModel)
        {
            var produto = await _produtoService.ObterPorId(id);

            if (produto == null) return NotFound();

            if (!ModelState.IsValid) return Resposta(ModelState);

            if(produtoModel.ArquivoEnviado != null)
            {
                var prefixoDaImagem = Guid.NewGuid() + "_";

                if (!await GuardarImagemEnviada(produtoModel.ArquivoEnviado, prefixoDaImagem))
                    return Resposta(produtoModel);

                RemoverImagemExistente(produto.Imagem);

                produtoModel.Imagem = prefixoDaImagem + produtoModel.ArquivoEnviado.FileName;
            }

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

        [NonAction]
        private async Task<bool> GuardarImagemEnviada(IFormFile arquivo, string prefixoDaImagem)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                NotificarErro("Forneça uma Imagem o produto!");
                return false;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/", prefixoDaImagem + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                NotificarErro("Já existe um Arquivo com Este Nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }

        [NonAction]
        public void RemoverImagemExistente(string arquivo)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/" + arquivo);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            NotificarErro("Arquivo Imagem não encontrado!");
        }

    }
}
