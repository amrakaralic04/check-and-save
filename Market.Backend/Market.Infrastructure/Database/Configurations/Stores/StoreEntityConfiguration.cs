using Market.Domain.Entities.StoreEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Stores;

public sealed class StoreEntityConfiguration : IEntityTypeConfiguration<StoreEntity>
{
    public void Configure(EntityTypeBuilder<StoreEntity> b)
    {
        b.ToTable("Store");

        b.HasKey(x => x.Id);

        b.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        b.Property(x => x.Contact)
            .HasMaxLength(100);

        b.Property(x => x.Email)
            .HasMaxLength(150);

        b.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        b.HasOne(x => x.CityEntity)
            .WithMany(c => c.Stores)
            .HasForeignKey(x => x.CityEntityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}