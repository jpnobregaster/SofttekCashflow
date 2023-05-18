using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Softtek.Cashflow.Infra.Persistence.Context.SqlServer;

namespace Softtek.Cashflow.Infra.IoC.DependencyInjection
{
    public static class DependencyInjectionIoC
    {

        public static void AddDependencyInjectionIoc(this IServiceCollection service)
        {
            service.AddDbContext<CashflowSqlServerContext>(options =>
            {
                options.UseInMemoryDatabase("Cashflow");
            });
        }
    }
}
