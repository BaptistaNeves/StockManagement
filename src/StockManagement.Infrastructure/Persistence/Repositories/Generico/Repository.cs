using Microsoft.EntityFrameworkCore;
using StockManagement.Core.Entities;
using StockManagement.Core.Interfaces.Persistence.Repositories.Generico;
using StockManagement.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Repositories.Generico
{
    public abstract class Repository<TEntidade> : IRepository<TEntidade> where TEntidade : Entidade
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<TEntidade> _dbSet;

        public IUnitOfWork UnitOfWork => _context;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntidade>();
        }

        public void Adicionar(TEntidade entidade)
        {
            _dbSet.Add(entidade);
        }

        public void Atualizar(TEntidade entidade)
        {
            _dbSet.Update(entidade);
        }

        public async Task Remover(Guid id)
        {
            _dbSet.Remove(await _dbSet.FindAsync(id));
        }

        public async Task<TEntidade> ObterPorId(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<ICollection<TEntidade>> ObterTodos()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<TEntidade>> Buscar(Expression<Func<TEntidade, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        

    }
}
