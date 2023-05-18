namespace Softtek.Cashflow.Domain.Common.Paging
{
    public interface IPaging<T>
    {
        IList<T> Data { get; set; }
        int Page { get; set; }
        int Count { get; set; }
        int Take { get; set; }
    }
}
