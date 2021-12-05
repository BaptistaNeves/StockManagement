using Microsoft.EntityFrameworkCore;
using StockManagement.Core.Entities.Catalogo;
using StockManagement.Core.Interfaces.Persistence.Repositories.Catalogo;
using StockManagement.Core.Interfaces.Persistence.Repositories.Generico;
using StockManagement.Infrastructure.Persistence.Context;
using StockManagement.Infrastructure.Persistence.Repositories.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Repositories.Catalogo
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context){}
        public IUnitOfWork unitOfWork => _context;

        public async Task<Produto> ObterProdutoVendasEstoquePorId(Guid id)
        {
            return await _context.Produtos.AsNoTracking()
                                 .Include(p => p.Vendas)
                                 .Include(p => p.Estoque)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
