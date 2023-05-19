namespace Softtek.Cashflow.Domain.ViewModel.Cashflow
{
    public class ConsolidatedBalanceViewModel
    {
        public DateTime Date { get; set; }
        public IList<decimal> Values { get; set; }  = new List<decimal>();
        public decimal Balance { get; set; }
    }
}
