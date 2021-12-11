using StockManagement.Application.InputModels.Catalogo;
using StockManagement.Core.DTOs.Catalogo;
using StockManagement.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockManagement.Application.Interface.Services.Catalogo
{
    public interface ICategoriaService : IDisposable
    {
        Task Adicionar(CategoriaInputModel categoria);
        Task Atualizar(CategoriaInputModel categoria);
        Task Remover(Guid id);
        Task<CategoriaDto> ObterPorId(Guid id);
        Task<PagedList<CategoriaDto>> ObterTodos(PaginationParams paginationParams);
    }
}
