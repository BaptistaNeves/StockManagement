using Microsoft.EntityFrameworkCore;
using StockManagement.Core.Entities.Catalogo;
using StockManagement.Core.Interfaces.Persistence.Repositories.Catalogo;
using StockManagement.Infrastructure.Persistence.Context;
using StockManagement.Infrastructure.Persistence.Repositories.Generico;
using StockManagement.Core.DTOs.Catalogo;
using StockManagement.Shared.Pagination;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Repositories.Catalogo
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context){}
        

        public async Task<Categoria> ObterCategoriaComProdutosPorId(Guid id)
        {
            return await _context.Categorias.AsNoTracking()
                                 .Include(c => c.Produtos).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<PagedList<CategoriaDto>> ObterCategorias(PaginationParams paginationParams)
        {
            var query = _context.Categorias.AsNoTracking()
                                .Select(categoria => new CategoriaDto 
                                { 
                                    Id = categoria.Id,
                                    Descricao = categoria.Descricao,
                                    DataCadastro = categoria.DataCadastro
                                });

            return await PagedList<CategoriaDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}
