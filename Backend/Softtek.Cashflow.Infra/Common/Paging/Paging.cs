using Softtek.Cashflow.Domain.Common.Paging;

namespace Softtek.Cashflow.Infra.Common.Paging
{
    internal class Paging<T> : IPaging<T>
    {
        public IList<T> Data { get; set; } = new List<T>();
        public int Page { get; set; }
        public int Count { get; set; }
        public int Take { get; set; }
    }
}
