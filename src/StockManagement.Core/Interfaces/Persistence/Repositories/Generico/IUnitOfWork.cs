using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Core.Interfaces.Persistence.Repositories.Generico
{
    public interface IUnitOfWork
    {
        Task<bool> Salvar();
    }
}
