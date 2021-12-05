using StockManagement.Core.Entities.Pessoa;
using StockManagement.Core.Interfaces.Persistence.Repositories.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Core.Interfaces.Persistence.Repositories.Pessoa
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> ObterClienteVendasPorId(Guid id);
    }
}
