namespace Market.Application.Modules.Identity.Activity.Queries.List
{
    public sealed class ListActivitiesQueryHandler(IAppDbContext ctx) : IRequestHandler<ListActivitiesQuery, PageResult<ListActivitiesQueryDto>>
    {
        public async Task<PageResult<ListActivitiesQueryDto>> Handle(ListActivitiesQuery request, CancellationToken ct)
        {
            var q = ctx.Activities.AsNoTracking();

            if (request.MarketUserEntityId.HasValue)
                q = q.Where(x => x.MarketUserEntityId == request.MarketUserEntityId.Value);

            if (!string.IsNullOrWhiteSpace(request.ActivityType))
            {
                var t = request.ActivityType.ToLower();
                q = q.Where(x => x.ActivityType.ToLower() == t);
            }

            var pq = q.Select(x => new ListActivitiesQueryDto
            {
                Id = x.Id,
                MarketUserEntityId = x.MarketUserEntityId,
                ActivityType = x.ActivityType,
                Description = x.Description,
                Date = x.Date
            });

            return await PageResult<ListActivitiesQueryDto>
                .FromQueryableAsync(pq, request.Page, ct);
        }
    }
}
