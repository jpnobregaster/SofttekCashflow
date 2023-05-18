using Microsoft.EntityFrameworkCore;

using Softtek.Cashflow.Domain.Common.Entities;
using Softtek.Cashflow.Domain.Common.Repository;

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
    }
}

