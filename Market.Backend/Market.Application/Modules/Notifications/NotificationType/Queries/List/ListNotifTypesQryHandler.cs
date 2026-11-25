namespace Market.Application.Modules.Notifications.NotificationType.Queries.List
{
    public sealed class ListNotifTypesQryHandler(IAppDbContext ctx) : IRequestHandler<ListNotifTypesQry, PageResult<ListNotifTypesQryDto>>
    {
        public async Task<PageResult<ListNotifTypesQryDto>> Handle(
       ListNotifTypesQry request, CancellationToken ct)
        {
            var q = ctx.NotificationTypes.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Search))
                q = q.Where(x => x.Name.Contains(request.Search));

            var projected = q.OrderBy(x => x.Name)
                .Select(x => new ListNotifTypesQryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                });

            return await PageResult<ListNotifTypesQryDto>
                .FromQueryableAsync(projected, request.Paging, ct);
        }
    }
}
