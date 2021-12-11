using StockManagement.Application.InputModels.Catalogo;
using StockManagement.Core.DTOs.Catalogo;
using StockManagement.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockManagement.Application.Interface.Services.Catalogo
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(ProdutoInputModel Produto);
        Task Atualizar(ProdutoInputModel Produto);
        Task Remover(Guid id);
        Task<ProdutoDto> ObterPorId(Guid id);
        Task<PagedList<ProdutoDto>> ObterTodos(PaginationParams paginationParams);
    }
}
