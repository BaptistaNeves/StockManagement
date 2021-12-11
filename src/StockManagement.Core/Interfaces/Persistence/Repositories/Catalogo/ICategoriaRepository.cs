using StockManagement.Core.DTOs.Catalogo;
using StockManagement.Core.Entities.Catalogo;
using StockManagement.Core.Interfaces.Persistence.Repositories.Generico;
using StockManagement.Shared.Pagination;
using System;
using System.Threading.Tasks;

namespace StockManagement.Core.Interfaces.Persistence.Repositories.Catalogo
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<Categoria> ObterCategoriaComProdutosPorId(Guid id);
        Task<PagedList<CategoriaDto>> ObterCategorias(PaginationParams paginationParams);
    }
}
