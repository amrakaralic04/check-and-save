
using Market.Domain.Entities.NotificationEntities;

namespace Market.Infrastructure.Database.Configurations.Notifications
{
    public sealed class NotificationTypeEntityConfiguration : IEntityTypeConfiguration<NotificationTypeEntity>
    {
        public void Configure(EntityTypeBuilder<NotificationTypeEntity> b)
        {
            b.ToTable("NotificationTypes");

            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(NotificationTypeEntity.Constraints.NameMaxLength);

            b.Property(x => x.Description)
                .HasMaxLength(NotificationTypeEntity.Constraints.DescriptionMaxLength);

            b.HasIndex(x => x.Name);
        }
    }
}
