namespace Market.Infrastructure.Database.Configurations.Identity;

public sealed class ActivityEntityConfiguration : IEntityTypeConfiguration<ActivityEntity>
{
    public void Configure(EntityTypeBuilder<ActivityEntity> b)
    {
        b.ToTable("Activities");

        b.HasKey(x => x.Id);

        b.Property(x => x.MarketUserEntityId)
            .IsRequired();

        b.Property(x => x.ActivityType)
            .IsRequired()
            .HasMaxLength(ActivityEntity.Constraints.ActivityTypeMaxLength);

        b.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(ActivityEntity.Constraints.DescriptionMaxLength);

        b.Property(x => x.Date)
            .IsRequired();

        
        b.HasIndex(x => x.MarketUserEntityId);
        b.HasIndex(x => x.Date);

       
        b.HasOne(x => x.MarketUserEntity)
            .WithMany() 
            .HasForeignKey(x => x.MarketUserEntityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
