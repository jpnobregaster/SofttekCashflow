using Microsoft.EntityFrameworkCore;

using Softtek.Cashflow.Domain.Entities.Transactions.Model;
using Softtek.Cashflow.Domain.Entities.Transactions.Repository;
using Softtek.Cashflow.Infra.Common.Repository;
using Softtek.Cashflow.Infra.Persistence.Context.SqlServer.Context;

namespace Softtek.Cashflow.Infra.Repositories.Transaction
{
    public class CashflowRepository : BaseRepository<CashflowModel>, ICashflowRepository
    {
        public CashflowRepository(CashflowSqlServerContext context) : base(context)
        {
        }

        public async Task<List<CashflowModel>> GetConsolidatedBalance()
        {
            var query = await _context.Set<CashflowModel>()
                .AsNoTracking()
                .Where(x => x.Consolidated)
                .ToListAsync();

            return query;
        }
    }
}
