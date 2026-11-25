namespace Market.Application.Modules.Notifications.NotificationType.Commands.Delete
{
    public sealed class DeleteNotifTypeCmd : IRequest<Unit>
    {
        public required int Id { get; set; }
    }
}
