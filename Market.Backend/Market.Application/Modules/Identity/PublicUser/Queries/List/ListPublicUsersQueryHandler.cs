namespace Market.Application.Modules.Identity.PublicUser.Queries.List;

public sealed class ListPublicUsersQueryHandler(IAppDbContext ctx)
    : IRequestHandler<ListPublicUsersQuery, PageResult<ListPublicUsersQueryDto>>
{
    public async Task<PageResult<ListPublicUsersQueryDto>> Handle(
        ListPublicUsersQuery request, CancellationToken ct)
    {
        var q = ctx.PublicUsers.AsNoTracking();

        if (request.MarketUserEntityId is not null)
            q = q.Where(x => x.MarketUserEntityId == request.MarketUserEntityId);

        if (request.MinPoints is not null)
            q = q.Where(x => x.Points >= request.MinPoints);

        if (request.MaxPoints is not null)
            q = q.Where(x => x.Points <= request.MaxPoints);

        var projectedQuery = q
            .OrderBy(x => x.MarketUserEntityId)
            .ThenBy(x => x.Id)
            .Select(x => new ListPublicUsersQueryDto
            {
                Id = x.Id,
                MarketUserEntityId = x.MarketUserEntityId,
                Points = x.Points,
                AvatarLevel = x.AvatarLevel
            });

        return await PageResult<ListPublicUsersQueryDto>.FromQueryableAsync(
            projectedQuery,
            request.Paging,
            ct);
    }
}