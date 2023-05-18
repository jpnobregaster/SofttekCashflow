using Microsoft.EntityFrameworkCore;

using Softtek.Cashflow.Domain.Entities.Transactions.Model;

namespace Softtek.Cashflow.Infra.Persistence.Context.SqlServer.Context
{
    public class CashflowSqlServerContext : DbContext
    {
        public DbSet<CashflowModel> CashFlows { get; set; }

        public CashflowSqlServerContext() : base()
        {
        }

        public CashflowSqlServerContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseInMemoryDatabase("Cashflow");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CashflowSqlServerContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}