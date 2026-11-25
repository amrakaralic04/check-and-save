namespace Market.Application.Modules.Identity.Manager.Queries.List
{
    public sealed class ListManagersQueryHandler(IAppDbContext ctx) : IRequestHandler<ListManagersQuery, PageResult<ListManagersQueryDto>>
    {
        public async Task<PageResult<ListManagersQueryDto>> Handle(ListManagersQuery request, CancellationToken ct)
        {
            var q = ctx.Managers.AsNoTracking();

            if (request.StoreEntityId.HasValue)
                q = q.Where(x => x.StoreEntityId == request.StoreEntityId.Value);

            if (request.MarketUserEntityId.HasValue)
                q = q.Where(x => x.MarketUserEntityId == request.MarketUserEntityId.Value);

            var pq = q.Select(x => new ListManagersQueryDto
            {
                Id = x.Id,
                MarketUserEntityId = x.MarketUserEntityId,
                StoreEntityId = x.StoreEntityId,
                StartDate = x.StartDate
            });

            return await PageResult<ListManagersQueryDto>
                .FromQueryableAsync(pq, request.Page, ct);
        }
    }
}
