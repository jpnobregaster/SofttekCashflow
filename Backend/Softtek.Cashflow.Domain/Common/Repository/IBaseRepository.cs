using Softtek.Cashflow.Domain.Common.Entities;
using Softtek.Cashflow.Domain.Common.Paging;

using System.Linq.Expressions;

namespace Softtek.Cashflow.Domain.Common.Repository
{
    public interface IBaseRepository<T>
        where T : Entity
    {
        Task AddAsync(T entity);
        Task<IPaging<T>> GetAllPagedAsync(Expression<Func<T, bool>> clauses, int page, int take);
    }
}
