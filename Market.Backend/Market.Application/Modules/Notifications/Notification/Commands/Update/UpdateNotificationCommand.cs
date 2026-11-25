namespace Market.Application.Modules.Notifications.Notification.Commands.Update
{
    public sealed class UpdateNotificationCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public required string Title { get; set; }
        public required string Message { get; set; }

        public bool IsRead { get; set; }
    }
}
