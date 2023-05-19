using Softtek.Cashflow.Domain.Common.Entities;

using System.Linq.Expressions;

namespace Softtek.Cashflow.Domain.Common.Repository
{
    public interface IBaseRepository<T>
        where T : Entity
    {
        Task AddAsync(T entity);
        Task<IList<T>> FindAsync(Expression<Func<T, bool>> clauses);
        Task UpdateAsync(T entity);
    }
}
