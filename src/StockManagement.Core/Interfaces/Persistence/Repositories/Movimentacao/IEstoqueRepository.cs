using StockManagement.Core.Entities.Movimentacao;
using StockManagement.Core.Interfaces.Persistence.Repositories.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Core.Interfaces.Persistence.Repositories.Movimentacao
{
    public interface IEstoqueRepository : IRepository<Estoque>
    {
    }
}
