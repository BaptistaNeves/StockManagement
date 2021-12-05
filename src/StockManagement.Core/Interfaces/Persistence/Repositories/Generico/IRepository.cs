using StockManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Core.Interfaces.Persistence.Repositories.Generico
{
    public interface IRepository<TEntidade>: IDisposable where TEntidade : Entidade
    {
        IUnitOfWork UnitOfWork { get; }
        void Adicionar(TEntidade entidade);
        void Atualizar(TEntidade entidade);
        Task Remover(Guid id);
        Task<TEntidade> ObterPorId(Guid id);
        Task<ICollection<TEntidade>> ObterTodos();
        Task<ICollection<TEntidade>> Buscar(Expression<Func<TEntidade, bool>> predicate);
    }
}
