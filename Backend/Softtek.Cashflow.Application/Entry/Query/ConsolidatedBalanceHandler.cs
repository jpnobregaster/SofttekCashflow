using MediatR;

using Softtek.Cashflow.Domain.Entities.Transactions.Model;
using Softtek.Cashflow.Domain.Entities.Transactions.Repository;
using Softtek.Cashflow.Domain.ViewModel.Cashflow;

namespace Softtek.Cashflow.Application.Entry.Query
{
    public class ConsolidatedBalanceHandler : IRequestHandler<ConsolidatedBalanceQuery, IList<CashflowModel>>
    {
        private readonly ICashflowRepository _transactionRepository;

        public ConsolidatedBalanceHandler(ICashflowRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IList<CashflowModel>> Handle(ConsolidatedBalanceQuery request, CancellationToken cancellationToken)
        {
            return await _transactionRepository.GetConsolidatedBalance();
        }
    }
}