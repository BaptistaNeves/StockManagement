using StockManagement.Application.InputModels.Movimentacao;
using StockManagement.Core.DTOs.Movimentacao;
using StockManagement.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Application.Interface.Services.Movimentacao
{
    public interface IEstoqueService : IDisposable
    {
        Task Adicionar(EstoqueInputModel estoque);
        Task Atualizar(EstoqueInputModel estoque);
        Task Remover(Guid id);
        Task<EstoqueDto> ObterPorId(Guid id);
        Task<PagedList<EstoqueDto>> ObterProdutosNoEstoques(PaginationParams paginationParams);
        Task<PagedList<EstoqueDto>> ObterProdutosEmAbaixaNoEstoque(PaginationParams paginationParams);
        Task<PagedList<EstoqueDto>> ObterProdutosVaziosNoEstoque(PaginationParams paginationParams);
    }
}
