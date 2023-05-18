using Softtek.Cashflow.Domain.Common.Repository;
using Softtek.Cashflow.Domain.Entities.Transactions.Model;

namespace Softtek.Cashflow.Domain.Entities.Transactions.Repository
{
    public interface ICashflowRepository: IBaseRepository<CashflowModel>
    {
        Task<List<CashflowModel>> GetConsolidatedBalance();
    }
}
