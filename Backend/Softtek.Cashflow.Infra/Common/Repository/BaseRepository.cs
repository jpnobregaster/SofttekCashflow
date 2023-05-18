using Microsoft.EntityFrameworkCore;

using Softtek.Cashflow.Domain.Common.Entities;
using Softtek.Cashflow.Domain.Common.Paging;
using Softtek.Cashflow.Domain.Common.Repository;
using Softtek.Cashflow.Infra.Common.Paging;

using System.Linq.Expressions;

namespace Softtek.Cashflow.Infra.Common.Repository
{
    abstract class BaseRepository<T> : IBaseRepository<T>
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

        public virtual async Task<IPaging<T>> GetAllPagedAsync(Expression<Func<T, bool>> clauses, int page, int take)
        {
            var query = this._context
                .Set<T>()
                .AsNoTracking()
                .AsQueryable();

            if (clauses != null)
                query = query.Where(clauses);

            return new Paging<T>
            {
                Page = page,
                Take = take,
                Count = await query.CountAsync(),
                Data = await query
                    .OrderBy(x => x.Id)
                    .Skip((page - 1) * take)
                    .Take(take)
                    .ToListAsync(),
            };
        }
    }
}

