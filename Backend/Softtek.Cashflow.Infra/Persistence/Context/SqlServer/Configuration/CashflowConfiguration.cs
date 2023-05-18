using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Softtek.Cashflow.Domain.Entities.Transactions.Model;

namespace Softtek.Cashflow.Infra.Persistence.Context.SqlServer.Configuration
{
    public class CashflowConfiguration : IEntityTypeConfiguration<CashflowModel>
    {
        public void Configure(EntityTypeBuilder<CashflowModel> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Value)
                .IsRequired()
                .HasColumnType("decimal(15, 2)");

            builder
                .Property(x => x.Consolidated);

            builder
                .Property(x => x.ConsolidatedAt)
                .IsRequired(false);

            builder
                .ToTable("Cashflow");
        }
    }
}
