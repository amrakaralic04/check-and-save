using Market.Domain.Entities.Analytics;

namespace Market.Infrastructure.Database.Configurations.Analytics
{
    public sealed class ReportEntityConfiguration : IEntityTypeConfiguration<ReportEntity>
    {
        public void Configure(EntityTypeBuilder<ReportEntity> b)
        {
            b.ToTable("Report");

            b.HasKey(x => x.Id);

            b.Property(x => x.ReportType)
                .IsRequired()
                .HasMaxLength(ReportEntity.Constraints.ReportTypeMaxLength);

            b.Property(x => x.Description)
                .HasMaxLength(ReportEntity.Constraints.DescriptionMaxLength);

            b.Property(x => x.ReportDate)
                .IsRequired();

            b.Property(x => x.CreatedAt)
                .IsRequired();

            b.Property(x => x.ModifiedAt);

            b.Property(x => x.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            b.HasOne(x => x.MarketUserEntity)
                .WithMany(u => u.Reports)
                .HasForeignKey(x => x.MarketUserEntityId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => new { x.MarketUserEntityId, x.ReportDate });
        }
    }
}