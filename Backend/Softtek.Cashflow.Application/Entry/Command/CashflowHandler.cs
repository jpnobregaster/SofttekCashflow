using AutoMapper;

using MediatR;

using Softtek.Cashflow.Domain.Entities.Transactions.Model;
using Softtek.Cashflow.Domain.Entities.Transactions.Repository;

namespace Softtek.Cashflow.Application.Entry.Command
{
    public class CashflowHandler : IRequestHandler<CashflowCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICashflowRepository _transactionRepository;

        public CashflowHandler(ICashflowRepository transactionRepository,
            IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task Handle(CashflowCommand request, CancellationToken cancellationToken)
        {
            var transaction = _mapper.Map<CashflowModel>(request);

            await _transactionRepository.AddAsync(transaction);
        }
    }
}
