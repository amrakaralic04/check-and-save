namespace Market.Application.Modules.Store.Stores.Queries.List;

public sealed class ListStoresQueryHandler(IAppDbContext ctx)
    : IRequestHandler<ListStoresQuery, PageResult<ListStoresQueryDto>>
{
    public async Task<PageResult<ListStoresQueryDto>> Handle(
        ListStoresQuery request, CancellationToken ct)
    {
        var q = ctx.Stores.AsNoTracking();

        if (request.CityEntityId is not null)
            q = q.Where(x => x.CityEntityId == request.CityEntityId);

        if (request.OnlyActive is not null)
            q = q.Where(x => x.IsActive == request.OnlyActive);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();
            q = q.Where(x =>
                x.Name.Contains(search) ||
                x.Contact.Contains(search) ||
                x.Email.Contains(search));
        }

        var projectedQuery = q
            .OrderBy(x => x.Name)
            .Select(x => new ListStoresQueryDto
            {
                Id = x.Id,
                Name = x.Name,
                Contact = x.Contact,
                Email = x.Email,
                IsActive = x.IsActive,
                CityEntityId = x.CityEntityId
            });

        return await PageResult<ListStoresQueryDto>.FromQueryableAsync(
            projectedQuery,
            request.Paging,
            ct);
    }
}