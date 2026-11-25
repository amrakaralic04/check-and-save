namespace Market.Infrastructure.Database.Configurations.Identity;

public sealed class UserEntityConfiguration : IEntityTypeConfiguration<MarketUserEntity>
{
    public void Configure(EntityTypeBuilder<MarketUserEntity> b)
    {
        b.ToTable("Users");

        b.HasKey(x => x.Id);

        b.HasIndex(x => x.Email).IsUnique();
        b.HasIndex(x => x.PublicId).IsUnique();

        b.Property(x => x.PublicId)
            .IsRequired()
            .HasMaxLength(MarketUserEntity.Constraints.PublicIdMaxLength);

        b.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200);

        b.Property(x => x.Firstname)
            .IsRequired()
            .HasMaxLength(100);

        b.Property(x => x.Lastname)
            .IsRequired()
            .HasMaxLength(100);

        b.Property(x => x.PasswordHash)
            .IsRequired();

        b.Property(x => x.RegistrationDate)
            .IsRequired();

        b.Property(x => x.IsAdmin).HasDefaultValue(false);
        b.Property(x => x.IsManager).HasDefaultValue(false);

        b.Property(x => x.IsPublicUser).HasDefaultValue(false);

        b.Property(x => x.TokenVersion).HasDefaultValue(0);
        b.Property(x => x.IsEnabled).HasDefaultValue(true);

        b.HasMany(x => x.RefreshTokens)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
