using Market.Domain.Entities.Analytics;

namespace Market.Infrastructure.Database.Configurations.Analytics
{
    public sealed class SalesStatisticEntityConfiguration : IEntityTypeConfiguration<SalesStatisticEntity>
    {
        public void Configure(EntityTypeBuilder<SalesStatisticEntity> b)
        {
            b.ToTable("SalesStatistic");

            b.HasKey(x => x.Id);

            b.Property(x => x.ViewsCount)
                .IsRequired();

            b.Property(x => x.SalesCount)
                .IsRequired();

            b.Property(x => x.Date)
                .IsRequired();

            b.Property(x => x.CreatedAt)
                .IsRequired();

            b.Property(x => x.ModifiedAt);

            b.Property(x => x.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            b.HasOne(x => x.ManagerEntity)
                .WithMany(m => m.SalesStatistics)
                .HasForeignKey(x => x.ManagerEntityId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.ProductEntity)
                .WithMany(p => p.SalesStatistics)
                .HasForeignKey(x => x.ProductEntityId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => new { x.ManagerEntityId, x.ProductEntityId, x.Date });
        }
    }
}