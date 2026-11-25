namespace Market.Application.Modules.Notifications.Notification.Commands.Create
{
    public sealed class CreateNotificationCommand : IRequest<int>
    {
        public int MarketUserEntityId { get; set; }
        public int NotificationTypeEntityId { get; set; }

        public required string Title { get; set; }
        public required string Message { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
