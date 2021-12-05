using StockManagement.Core.Entities.Vendas;
using StockManagement.Core.Interfaces.Persistence.Repositories.Generico;
using StockManagement.Core.Interfaces.Persistence.Repositories.Vendas;
using StockManagement.Infrastructure.Persistence.Context;
using StockManagement.Infrastructure.Persistence.Repositories.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Repositories.Vendas
{
    public class VendaRepository : Repository<Venda>, IVendaRepository
    {
        public VendaRepository(AppDbContext context) : base(context) {}
        public IUnitOfWork unitOfWork => _context;
    }
}
