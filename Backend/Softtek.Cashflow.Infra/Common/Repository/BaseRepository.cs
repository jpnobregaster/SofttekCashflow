using Microsoft.EntityFrameworkCore;

using Softtek.Cashflow.Domain.Common.Entities;
using Softtek.Cashflow.Domain.Common.Repository;

using System.Linq.Expressions;

namespace Softtek.Cashflow.Infra.Common.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : Entity
    {
        protected readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public virtual async Task AddAsync(T entity)
        {
            _context
                .Set<T>()
                .Add(entity);

            await _context.SaveChangesAsync();
        }

        public virtual async Task<IList<T>> FindAsync(Expression<Func<T, bool>> clauses)
        {
            return await this._context
                .Set<T>()
                .AsNoTracking()
                .Where(clauses)
                .ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            this._context
                .Entry(entity).State = EntityState.Modified;

            this._context
                .Set<T>()
                .Update(entity);

            await this._context.SaveChangesAsync();
        }
    }
}

