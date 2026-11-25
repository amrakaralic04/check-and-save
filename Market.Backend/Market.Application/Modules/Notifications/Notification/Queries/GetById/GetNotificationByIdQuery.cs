
namespace Market.Application.Modules.Notifications.Notification.Queries.GetById
{
    public sealed class GetNotificationByIdQuery : IRequest<GetNotificationByIdQueryDto>
    {
        public int Id { get; set; }
    }
}
