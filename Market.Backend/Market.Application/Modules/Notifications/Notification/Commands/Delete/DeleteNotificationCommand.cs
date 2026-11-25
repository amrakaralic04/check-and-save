namespace Market.Application.Modules.Notifications.Notification.Commands.Delete
{
    public sealed class DeleteNotificationCommand : IRequest<Unit>
    {
        public required int Id { get; set; }
    }
}
