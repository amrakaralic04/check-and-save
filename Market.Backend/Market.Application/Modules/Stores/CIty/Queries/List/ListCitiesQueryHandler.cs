

namespace Market.Application.Modules.Stores.City.Queries.List;

public sealed class ListCitiesQueryHandler(IAppDbContext ctx)
    : IRequestHandler<ListCitiesQuery, PageResult<ListCitiesQueryDto>>
{
    public async Task<PageResult<ListCitiesQueryDto>> Handle(
        ListCitiesQuery request, CancellationToken ct)
    {
        var q = ctx.Cities.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();
            q = q.Where(x => x.Name.Contains(search));
        }

        if (request.PostalCode is not null)
        {
            q = q.Where(x => x.PostalCode == request.PostalCode);
        }

        var projectedQuery = q
            .OrderBy(x => x.Name)
            .Select(x => new ListCitiesQueryDto
            {
                Id = x.Id,
                Name = x.Name,
                PostalCode = x.PostalCode
            });

        return await PageResult<ListCitiesQueryDto>.FromQueryableAsync(
            projectedQuery,
            request.Paging,
            ct);
    }
}