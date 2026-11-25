namespace Market.Application.Modules.Notifications.Notification.Queries.GetById
{
    public sealed class GetNotificationByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetNotificationByIdQuery, GetNotificationByIdQueryDto>
    {
        public async Task<GetNotificationByIdQueryDto> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var notification = await context.Notifications
                .Where(n => n.Id == request.Id)
                .Select(x => new GetNotificationByIdQueryDto
                {
                    Id = x.Id,
                    MarketUserEntityId = x.MarketUserEntityId,
                    NotificationTypeEntityId = x.NotificationTypeEntityId,
                    Title = x.Title,
                    Message = x.Message,
                    IsRead = x.IsRead
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (notification == null)
            {
                throw new MarketNotFoundException($"Notification with Id {request.Id} not found.");
            }

            return notification;
        }
    }
}
