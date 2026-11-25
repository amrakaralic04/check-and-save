using Market.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Infrastructure.Database.Configurations.Products
{
    public sealed class FavoritesEntityConfiguration : IEntityTypeConfiguration<FavoritesEntity>
    {
        public void Configure(EntityTypeBuilder<FavoritesEntity> b)
        {
            b.ToTable("Favorites");

            b.HasKey(x => x.Id);

            b.Property(x => x.DateAdded)
                .IsRequired();

           
            b.HasOne(x => x.ProductEntity)
                .WithMany(x => x.Favorites)
                .HasForeignKey(x => x.ProductEntityId);

            
            b.HasOne(x => x.PublicUserEntity)
                .WithMany()
                .HasForeignKey(x => x.PublicUserEntityId);
        }
    }
}
