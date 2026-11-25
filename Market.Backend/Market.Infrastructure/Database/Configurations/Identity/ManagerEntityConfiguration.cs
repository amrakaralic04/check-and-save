namespace Market.Infrastructure.Database.Configurations.Identity;

public sealed class ManagerEntityConfiguration : IEntityTypeConfiguration<ManagerEntity>
{
    public void Configure(EntityTypeBuilder<ManagerEntity> b)
    {
        b.ToTable("Managers");

        b.HasKey(x => x.Id);

        b.Property(x => x.MarketUserEntityId)
            .IsRequired();

        b.Property(x => x.StoreEntityId)
            .IsRequired();

        b.Property(x => x.StartDate)
            .IsRequired();

      
        b.HasIndex(x => x.MarketUserEntityId)
            .IsUnique();

        b.HasOne(x => x.MarketUserEntity)
            .WithOne(x => x.ManagerEntity) 
            .HasForeignKey<ManagerEntity>(x => x.MarketUserEntityId)
            .OnDelete(DeleteBehavior.Cascade);

        
        b.HasIndex(x => x.StoreEntityId);

        b.HasOne(x => x.StoreEntity)
            .WithMany() 
            .HasForeignKey(x => x.StoreEntityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

