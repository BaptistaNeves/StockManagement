using StockManagement.Core.Entities.Catalogo;
using StockManagement.Core.Interfaces.Persistence.Repositories.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Core.Interfaces.Persistence.Repositories.Catalogo
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> ObterProdutoVendasEstoquePorId(Guid id);
    }
}
