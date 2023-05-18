using Softtek.Cashflow.Domain.Common.Entities;

namespace Softtek.Cashflow.Domain.Entities.Transactions.Model
{
    public class CashflowModel : Entity
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Value { get; set; }
        public bool Consolidated { get; set; }
        public DateTime? ConsolidatedAt { get; set; }
    }
}
