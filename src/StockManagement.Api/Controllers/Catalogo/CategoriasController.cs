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
    public class CategoriasController : MainController
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriasController(INotificador noticador, 
                                   ICategoriaService categoriaService) : base(noticador)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet("obter-categorias")]
        public async Task<ActionResult<ICollection<CategoriaViewModel>>> ObterTodos()
        {
            return Ok(await _categoriaService.ObterTodos());
        }

        [HttpGet("obter-categoria-por-id/{id:guid}")]
        public async Task<ActionResult<CategoriaViewModel>> ObterPorId(Guid id)
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
