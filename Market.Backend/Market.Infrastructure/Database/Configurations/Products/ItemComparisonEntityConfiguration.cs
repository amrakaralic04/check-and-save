using Market.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Infrastructure.Database.Configurations.Products
{
    public sealed class ItemComparisonEntityConfiguration : IEntityTypeConfiguration<ItemComparisonEntity>
    {
        public void Configure(EntityTypeBuilder<ItemComparisonEntity> b)
        {
            b.ToTable("ItemComparisons");

            b.HasKey(x => x.Id);

            
            b.HasOne(x => x.ComparisonEntity)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.ComparisonEntityId);

            b.HasOne(x => x.ProductEntity)
                .WithMany(x => x.ItemComparison)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
