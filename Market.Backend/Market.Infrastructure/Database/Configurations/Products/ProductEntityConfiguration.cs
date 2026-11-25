using Market.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Infrastructure.Database.Configurations.Products
{
    public sealed class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> b)
        {
            b.ToTable("Products");

            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            b.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(2000);

            b.Property(x => x.ImageURL)
                .HasMaxLength(500);

            b.Property(x => x.DateAdded)
                .IsRequired();

            b.HasOne(x => x.StoreEntity)
                .WithMany()
                .HasForeignKey(x => x.StoreEntityId);

            b.HasOne(x => x.BranchEntity)
                .WithMany()
                .HasForeignKey(x => x.BranchEntityId);

            b.HasOne(x => x.CategoryEntity)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryEntityId);

            b.HasOne(x => x.BrandEntity)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.BrandEntityId);

            b.HasMany(x => x.Prices)
                .WithOne(x => x.ProductEntity!)
                .HasForeignKey(x => x.ProductEntityId);

            b.HasMany(x => x.Reviews)
                .WithOne(x => x.ProductEntity!)
                .HasForeignKey(x => x.ProductEntityId);

            b.HasMany(x => x.Favorites)
                .WithOne(x => x.ProductEntity!)
                .HasForeignKey(x => x.ProductEntityId);

            b.HasMany(x => x.ItemComparison)
                .WithOne(x => x.ProductEntity!)
                .HasForeignKey(x => x.ProductId);
        }
    }

}
