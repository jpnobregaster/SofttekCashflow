using Softtek.Cashflow.Domain.Common.Entities;

namespace Softtek.Cashflow.Domain.Common.Repository
{
    public interface IBaseRepository<T>
        where T : Entity
    {
        Task AddAsync(T entity);
    }
}
