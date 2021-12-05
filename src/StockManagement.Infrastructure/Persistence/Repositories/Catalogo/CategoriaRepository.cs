using Microsoft.EntityFrameworkCore;
using StockManagement.Core.Entities.Catalogo;
using StockManagement.Core.Interfaces.Persistence.Repositories.Catalogo;
using StockManagement.Infrastructure.Persistence.Context;
using StockManagement.Infrastructure.Persistence.Repositories.Generico;
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
    }
}
