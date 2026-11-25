using Market.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Infrastructure.Database.Configurations.Products
{
    public sealed class ReviewEntityConfiguration : IEntityTypeConfiguration<ReviewEntity>
    {
        public void Configure(EntityTypeBuilder<ReviewEntity> b)
        {
            b.ToTable("Reviews");

            b.HasKey(x => x.Id);

            b.Property(x => x.Rating)
                .IsRequired();

            b.Property(x => x.Comment)
                .HasMaxLength(2000);

            b.Property(x => x.Date)
                .IsRequired();

            b.HasOne(x => x.ProductEntity)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.ProductEntityId);

            b.HasOne(x => x.PublicUserEntity)
                .WithMany()
                .HasForeignKey(x => x.PublicUserEntityId);
        }
    }

}
