namespace Market.Application.Modules.Notifications.NotificationType.Commands.Update
{
    public sealed class UpdateNotifTypeCmd : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
