using Quartz;

using Softtek.Cashflow.Domain.Entities.Transactions.Repository;
using Softtek.Cashflow.Domain.Jobs;

namespace Softtek.Cashflow.Infra.Jobs
{
    public class ConsolidationSchaduleJob : IConsolidationSchaduleJob
    {
        private readonly ICashflowRepository _cashflowRepository;

        public ConsolidationSchaduleJob(ICashflowRepository cashflowRepository)
        {
            _cashflowRepository = cashflowRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var nonConsolidatedCashflows = await _cashflowRepository.FindAsync(x => !x.Consolidated);

            foreach (var nonConsolidatedCashflow in nonConsolidatedCashflows)
            {
                nonConsolidatedCashflow.Consolidated = true;
                nonConsolidatedCashflow.ConsolidatedAt = DateTime.Now;

                await _cashflowRepository.UpdateAsync(nonConsolidatedCashflow);
            }
        }
    }
}