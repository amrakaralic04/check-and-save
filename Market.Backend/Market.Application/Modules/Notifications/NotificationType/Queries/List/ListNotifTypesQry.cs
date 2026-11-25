namespace Market.Application.Modules.Notifications.NotificationType.Queries.List
{
    public sealed class ListNotifTypesQry : BasePagedQuery<ListNotifTypesQryDto>
    {
        public string? Search { get; init; }
    }
}
