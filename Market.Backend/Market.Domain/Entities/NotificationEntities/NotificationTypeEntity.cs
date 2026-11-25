
using Market.Domain.Common;

namespace Market.Domain.Entities.NotificationEntities
{
    public sealed class NotificationTypeEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;        
        public string? Description { get; set; }                

        public ICollection<NotificationEntity> Notifications { get; private set; } = new List<NotificationEntity>();

        public static class Constraints
        {
            public const int NameMaxLength = 150;
            public const int DescriptionMaxLength = 1000;
        }
    }
}
