using Microsoft.EntityFrameworkCore;
using StockManagement.Core.Entities.Vendas;
using StockManagement.Core.Interfaces.Persistence.Repositories.Vendas;
using StockManagement.Infrastructure.Persistence.Context;
using StockManagement.Infrastructure.Persistence.Repositories.Generico;
using System;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Repositories.Vendas
{
    public class VendaProdutoRepository : Repository<VendaProduto>, IVendaProdutoRepository
    {
        public VendaProdutoRepository(AppDbContext context) : base(context){}

        public async Task<VendaProduto> ObterVendaProdutoPorVendaId(Guid id)
        {
            return await _context.VendaProdutos.AsNoTracking()
                                .FirstOrDefaultAsync(vp => vp.VendaId == id);
        }
    }
}
