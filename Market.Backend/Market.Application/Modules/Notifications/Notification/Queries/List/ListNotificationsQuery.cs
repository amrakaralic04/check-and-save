
namespace Market.Application.Modules.Notifications.Notification.Queries.List
{
    public sealed class ListNotificationsQuery : BasePagedQuery<ListNotificationsQueryDto>
    {
        public string? Search { get; init; }
        public bool? OnlyUnread { get; init; }
    }
}
