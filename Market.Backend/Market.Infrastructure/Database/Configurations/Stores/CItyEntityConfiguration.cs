using Market.Domain.Entities.StoreEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Stores;

public sealed class CityEntityConfiguration : IEntityTypeConfiguration<CityEntity>
{
    public void Configure(EntityTypeBuilder<CityEntity> b)
    {
        b.ToTable("City");

        b.HasKey(x => x.Id);

        b.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        b.Property(x => x.PostalCode)
            .IsRequired();

        b.HasMany(x => x.Stores)
            .WithOne(x => x.CityEntity)
            .HasForeignKey(x => x.CityEntityId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasMany(x => x.Branches)
            .WithOne(x => x.CityEntity)
            .HasForeignKey(x => x.CityEntityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}