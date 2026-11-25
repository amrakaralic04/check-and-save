using Market.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Infrastructure.Database.Configurations.Products
{
    public sealed class ComparisonEntityConfiguration : IEntityTypeConfiguration<ComparisonEntity>
    {
        public void Configure(EntityTypeBuilder<ComparisonEntity> b)
        {
            b.ToTable("Comparisons");

            b.HasKey(x => x.Id);

            b.Property(x => x.CustomerEntityId)
                .IsRequired();

            b.Property(x => x.Date)
                .IsRequired();

            b.HasMany(x => x.Items)
                .WithOne(x => x.ComparisonEntity!)
                .HasForeignKey(x => x.ComparisonEntityId);
        }
    }

}
