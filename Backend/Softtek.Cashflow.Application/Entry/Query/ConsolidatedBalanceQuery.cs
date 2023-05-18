using MediatR;

using Softtek.Cashflow.Domain.ViewModel.Cashflow;

namespace Softtek.Cashflow.Application.Entry.Query
{
    public class ConsolidatedBalanceQuery : IRequest<IList<ConsolidatedBalanceViewModel>>
    {
    }
}
