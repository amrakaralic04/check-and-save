namespace Market.Infrastructure.Database.Configurations.Identity;

public sealed class PublicUserEntityConfiguration
    : IEntityTypeConfiguration<PublicUserEntity>
{
    public void Configure(EntityTypeBuilder<PublicUserEntity> b)
    {
        b.ToTable("PublicUsers");

        b.HasKey(x => x.Id);

        b.HasOne(x => x.MarketUserEntity)
            .WithOne(x => x.PublicUserEntity)
            .HasForeignKey<PublicUserEntity>(x => x.MarketUserEntityId)
            .OnDelete(DeleteBehavior.Cascade);

        b.Property(x => x.Points)
            .HasDefaultValue(PublicUserEntity.Constraints.MinPoints);

        b.Property(x => x.AvatarLevel)
            .HasDefaultValue(PublicUserEntity.Constraints.MinAvatarLevel);
    }
}