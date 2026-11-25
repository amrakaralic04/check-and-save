namespace Market.Application.Modules.Notifications.NotificationType.Queries.GetById
{
    public sealed class GetNotifTypeByIdQry : IRequest<GetNotifTypeByIdQryDto>
    {
        public int Id { get; set; }
    }
}
