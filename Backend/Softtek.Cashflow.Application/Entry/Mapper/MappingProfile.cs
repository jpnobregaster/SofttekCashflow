using AutoMapper;

using Softtek.Cashflow.Application.Entry.Command;
using Softtek.Cashflow.Domain.Entities.Transactions.Model;
using Softtek.Cashflow.Domain.ViewModel.Cashflow;

namespace Softtek.Cashflow.Application.Entry.Mapper
{
    public class EntryMappingProfile : Profile
    {
        public EntryMappingProfile()
        {
            CreateMap<CashflowCommand, CashflowModel>();

            CreateMap<IList<CashflowModel>, IList<ConsolidatedBalanceViewModel>>()
                .ConvertUsing<ConsolidatedDailyBalanceConverter>();
        }
    }

    public class ConsolidatedDailyBalanceConverter : ITypeConverter<IList<CashflowModel>, IList<ConsolidatedBalanceViewModel>>
    {
        public IList<ConsolidatedBalanceViewModel> Convert(IList<CashflowModel> source, IList<ConsolidatedBalanceViewModel> destination, ResolutionContext context)
        {
            return source.GroupBy(x => x.ConsolidatedAt.Value.Date)
                .Select(flow =>
                {
                    return new ConsolidatedBalanceViewModel
                    {
                        Date = flow.Key.Date,
                        Balance = flow.Sum(x => x.Value)
                    };
                })
                .ToList();
        }
    }
}
