using StockManagement.Core.DTOs.Movimentacao;
using StockManagement.Core.Entities.Movimentacao;
using StockManagement.Core.Interfaces.Persistence.Repositories.Generico;
using StockManagement.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Core.Interfaces.Persistence.Repositories.Movimentacao
{
    public interface IEstoqueRepository : IRepository<Estoque>
    {
        Task<PagedList<EstoqueDto>> ObterProdutosNoEstoques(PaginationParams pagainationParams);
        Task<PagedList<EstoqueDto>> ObterProdutosEmAbaixaNoEstoque(PaginationParams pagainationParams);
        Task<PagedList<EstoqueDto>> ObterProdutosVaziosNoEstoque(PaginationParams pagainationParams);
        Task DecrementarQuantidadeProdutoNoEstoque(Guid produtoId, int quantidade);
    }
}
