namespace Market.Application.Modules.Notifications.Notification.Queries.List
{
    public sealed class ListNotificationsQueryHandler(IAppDbContext ctx) : IRequestHandler<ListNotificationsQuery, PageResult<ListNotificationsQueryDto>>
    {
        public async Task<PageResult<ListNotificationsQueryDto>> Handle(
       ListNotificationsQuery request, CancellationToken ct)
        {
            var q = ctx.Notifications.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                q = q.Where(x => x.Title.Contains(request.Search));
            }

            if (request.OnlyUnread is not null)
            {
                q = q.Where(x => x.IsRead == !request.OnlyUnread);
            }

            var projectedQuery = q
                .OrderByDescending(x => x.Id)
                .Select(x => new ListNotificationsQueryDto
                {
                    Id = x.Id,
                    MarketUserEntityId = x.MarketUserEntityId,
                    NotificationTypeEntityId = x.NotificationTypeEntityId,
                    Title = x.Title,
                    IsRead = x.IsRead
                });

            return await PageResult<ListNotificationsQueryDto>
                .FromQueryableAsync(projectedQuery, request.Paging, ct);
        }
    }
}
