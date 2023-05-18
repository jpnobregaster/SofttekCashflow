using AutoMapper;

using Softtek.Cashflow.Application.Entry.Command;
using Softtek.Cashflow.Domain.Entities.Transactions.Model;

namespace Softtek.Cashflow.Application.Entry.Mapper
{
    public class EntryMappingProfile : Profile
    {
        public EntryMappingProfile()
        {
            CreateMap<CashflowCommand, CashflowModel>();
        }
    }
}
