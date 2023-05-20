using MediatR;

using Softtek.Cashflow.Domain.Entities.Transactions.Model;

namespace Softtek.Cashflow.Application.Entry.Query
{
    public class ConsolidatedBalanceQuery : IRequest<IList<CashflowModel>>
    {
    }
}
