using StockManagement.Core.Entities.Movimentacao;
using StockManagement.Core.Interfaces.Persistence.Repositories.Generico;
using StockManagement.Core.Interfaces.Persistence.Repositories.Movimentacao;
using StockManagement.Infrastructure.Persistence.Context;
using StockManagement.Infrastructure.Persistence.Repositories.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Repositories.Movimentacao
{
    public class EstoqueRepository : Repository<Estoque>, IEstoqueRepository
    {
        public EstoqueRepository(AppDbContext context) : base(context){}
        public IUnitOfWork unitOfWork => _context;
    }
}
