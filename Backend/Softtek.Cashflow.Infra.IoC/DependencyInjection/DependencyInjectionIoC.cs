using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Softtek.Cashflow.Application.Entry.Command;
using Softtek.Cashflow.Application.Entry.Mapper;
using Softtek.Cashflow.Domain.Entities.Transactions.Repository;
using Softtek.Cashflow.Infra.Persistence.Context.SqlServer.Context;
using Softtek.Cashflow.Infra.Repositories.Transaction;

using System.Reflection;

namespace Softtek.Cashflow.Infra.IoC.DependencyInjection
{
    public static class DependencyInjectionIoC
    {
        public static void AddDependencyInjectionIoC(this IServiceCollection services)
        {
            services.AddDbContext<CashflowSqlServerContext>(options =>
            {
                options.UseInMemoryDatabase("Cashflow");
            });

            services.AddMediatR(options => {
                options.RegisterServicesFromAssembly(typeof(CashflowCommand).GetTypeInfo().Assembly);
            });

            services.AddValidatorsFromAssembly(typeof(CashflowCommand).GetTypeInfo().Assembly);

            services.AddAutoMapper(typeof(EntryMappingProfile).GetTypeInfo().Assembly);

            services.AddScoped<ICashflowRepository, CashflowRepository>();
        }
    }
}
