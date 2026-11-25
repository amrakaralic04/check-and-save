using Market.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Infrastructure.Database.Configurations.Products
{
    public sealed class PriceEntityConfiguration : IEntityTypeConfiguration<PriceEntity>
    {
        public void Configure(EntityTypeBuilder<PriceEntity> b)
        {
            b.ToTable("Prices");

            b.HasKey(x => x.Id);

            b.Property(x => x.Amount)
                .IsRequired();

            b.Property(x => x.DateUpdated)
                .IsRequired();

            b.HasOne(x => x.ProductEntity)
                .WithMany(x => x.Prices)
                .HasForeignKey(x => x.ProductEntityId);
        }
    }
}
