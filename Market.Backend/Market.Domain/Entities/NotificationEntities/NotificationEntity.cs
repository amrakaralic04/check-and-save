using Market.Domain.Common;
using Market.Domain.Entities.Identity;
namespace Market.Domain.Entities.NotificationEntities
{
    public sealed class NotificationEntity : BaseEntity
    {

        public int MarketUserEntityId { get; set; }
        public MarketUserEntity? MarketUserEntity { get; set; }


        public int NotificationTypeEntityId { get; set; }
        public NotificationTypeEntity? NotificationType { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }

        public static class Constraints
        {
            public const int TitleMaxLength = 200;
            public const int MessageMaxLength = 2000;
        }
    }
}
