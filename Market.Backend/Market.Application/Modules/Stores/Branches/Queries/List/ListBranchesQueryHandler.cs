namespace Market.Application.Modules.Stores.Branches.Queries.List;

public sealed class ListBranchesQueryHandler(IAppDbContext ctx)
    : IRequestHandler<ListBranchesQuery, PageResult<ListBranchesQueryDto>>
{
    public async Task<PageResult<ListBranchesQueryDto>> Handle(
        ListBranchesQuery request, CancellationToken ct)
    {
        var q = ctx.Branches.AsNoTracking();

        if (request.StoreEntityId is not null)
            q = q.Where(x => x.StoreEntityId == request.StoreEntityId);

        if (request.CityEntityId is not null)
            q = q.Where(x => x.CityEntityId == request.CityEntityId);

        if (request.OnlyActive is not null)
            q = q.Where(x => x.IsActive == request.OnlyActive);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();
            q = q.Where(x =>
                x.Address.Contains(search) ||
                x.Contact.Contains(search) ||
                x.Email.Contains(search));
        }

        var projectedQuery = q
            .OrderBy(x => x.StoreEntityId)
            .ThenBy(x => x.CityEntityId)
            .ThenBy(x => x.Address)
            .Select(x => new ListBranchesQueryDto
            {
                Id = x.Id,
                StoreEntityId = x.StoreEntityId,
                CityEntityId = x.CityEntityId,
                Address = x.Address,
                Contact = x.Contact,
                Email = x.Email,
                IsActive = x.IsActive
            });

        return await PageResult<ListBranchesQueryDto>.FromQueryableAsync(
            projectedQuery,
            request.Paging,
            ct);
    }
}