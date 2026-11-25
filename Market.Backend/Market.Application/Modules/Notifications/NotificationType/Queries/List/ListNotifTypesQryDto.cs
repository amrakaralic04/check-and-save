namespace Market.Application.Modules.Notifications.NotificationType.Queries.List
{
    public sealed class ListNotifTypesQryDto
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public string? Description { get; init; }
    }
}
