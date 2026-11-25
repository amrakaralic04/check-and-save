
using Market.Domain.Entities.NotificationEntities;

namespace Market.Infrastructure.Database.Configurations.Notifications
{
    public sealed class NotificationEntityConfiguration : IEntityTypeConfiguration<NotificationEntity>
    {
        public void Configure(EntityTypeBuilder<NotificationEntity> b)
        {
            b.ToTable("Notifications");

            b.HasKey(x => x.Id);

            b.Property(x => x.MarketUserEntityId)
                .IsRequired();

            b.Property(x => x.NotificationTypeEntityId)
                .IsRequired();

            b.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(NotificationEntity.Constraints.TitleMaxLength);

            b.Property(x => x.Message)
                .IsRequired()
                .HasMaxLength(NotificationEntity.Constraints.MessageMaxLength);

            b.Property(x => x.IsRead)
                .IsRequired();

            b.HasIndex(x => x.MarketUserEntityId);
            b.HasIndex(x => x.NotificationTypeEntityId);
            b.HasIndex(x => x.IsRead);

            b.HasOne(x => x.MarketUserEntity)
                .WithMany(u => u.Notifications)
                .HasForeignKey(x => x.MarketUserEntityId)
                .OnDelete(DeleteBehavior.Cascade);


            b.HasOne(x => x.NotificationType)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.NotificationTypeEntityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
