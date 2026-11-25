using Market.Domain.Entities.StoreEntities;

namespace Market.Infrastructure.Database.Configurations.Stores;

public sealed class BranchEntityConfiguration : IEntityTypeConfiguration<BranchEntity>
{
    public void Configure(EntityTypeBuilder<BranchEntity> b)
    {
        b.ToTable("Branch");
        b.HasKey(x => x.Id);

        b.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(200);

        b.Property(x => x.Contact)
            .HasMaxLength(100);

        b.Property(x => x.Email)
            .HasMaxLength(150);

        b.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        b.Property(x => x.Latitude)
            .HasColumnType("float");

        b.Property(x => x.Longitude)
            .HasColumnType("float");

        b.HasOne(x => x.StoreEntity)
            .WithMany(s => s.Branches)
            .HasForeignKey(x => x.StoreEntityId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(x => x.CityEntity)
            .WithMany(c => c.Branches)
            .HasForeignKey(x => x.CityEntityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}