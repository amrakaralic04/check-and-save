
namespace Market.Application.Modules.Notifications.Notification.Queries.List
{
    public sealed class ListNotificationsQueryDto
    {
        public required int Id { get; init; }

        public required int MarketUserEntityId { get; init; }
        public required int NotificationTypeEntityId { get; init; }

        public required string Title { get; init; }
        public required bool IsRead { get; init; }
    }
}
