using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Extensions;
using StockManagement.Application.InputModels.Catalogo;
using StockManagement.Application.Interface.Services.Catalogo;
using StockManagement.Core.DTOs.Catalogo;
using StockManagement.Core.Interfaces.Notification;
using StockManagement.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockManagement.Api.Controllers.Catalogo
{
    public class CategoriasController : MainController
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriasController(INotificador noticador, 
                                   ICategoriaService categoriaService) : base(noticador)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet("obter-categorias")]
        public async Task<ActionResult<ICollection<CategoriaDto>>> ObterTodos([FromQuery]PaginationParams paginationParams)
        {
            var categorias = await _categoriaService.ObterTodos(paginationParams);

            Response.AddPaginationHeader(categorias.CurrentPage, categorias.PageSize, categorias.TotalCount,
                categorias.TotalPages);

            return Ok(categorias);
        }

        [HttpGet("obter-categoria-por-id/{id:guid}")]
        public async Task<ActionResult<CategoriaDto>> ObterPorId(Guid id)
        {
            return Ok(await _categoriaService.ObterPorId(id));
        }

        [HttpPost("nova-categoria")]
        public async Task<ActionResult> Adicionar(CategoriaInputModel categoriaModel)
        {
            if (!ModelState.IsValid) return Resposta(ModelState);

            await _categoriaService.Adicionar(categoriaModel);

            return Resposta(categoriaModel);
        }

        [HttpPut("editar-categoria/{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, CategoriaInputModel categoriaModel)
        {
            if (!ModelState.IsValid) return Resposta(ModelState);

            await _categoriaService.Atualizar(categoriaModel);

            return Resposta(categoriaModel);
        }

        [HttpDelete("remover-categoria/{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var categoria = await _categoriaService.ObterPorId(id);

            if (categoria == null) return NotFound();

            await _categoriaService.Remover(id);

            return Resposta();
        }

    }
}
