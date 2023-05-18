using MediatR;

using Softtek.Cashflow.Domain.Entities.Transactions.Repository;
using Softtek.Cashflow.Domain.ViewModel.Cashflow;

namespace Softtek.Cashflow.Application.Entry.Query
{
    public class ConsolidatedBalanceHandler : IRequestHandler<ConsolidatedBalanceQuery, IList<ConsolidatedBalanceViewModel>>
    {
        private readonly ICashflowRepository _transactionRepository;

        public ConsolidatedBalanceHandler(ICashflowRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IList<ConsolidatedBalanceViewModel>> Handle(ConsolidatedBalanceQuery request, CancellationToken cancellationToken)
        {
            var consolidatedCashFlows = await _transactionRepository.GetConsolidatedBalance();

            return consolidatedCashFlows
                .GroupBy(x => x.ConsolidatedAt)
                .Select(flow =>
                {
                    return new ConsolidatedBalanceViewModel
                    {
                        Date = flow.Key.Value,
                        Balance = flow.Sum(x => x.Value)
                    };
                })
                .ToList();
        }
    }
}