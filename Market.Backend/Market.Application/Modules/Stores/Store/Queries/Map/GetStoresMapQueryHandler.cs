namespace Market.Application.Modules.Store.Stores.Queries.Map;

public sealed class GetStoresMapQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetStoresMapQuery, List<StoreMapItemDto>>
{
    public async Task<List<StoreMapItemDto>> Handle(
        GetStoresMapQuery request,
        CancellationToken ct)
    {
        return await ctx.Branches
            .AsNoTracking()
            .Where(b => !b.IsDeleted
                        && b.IsActive
                        && b.Latitude.HasValue
                        && b.Longitude.HasValue)
            .Include(b => b.StoreEntity)
            .Select(b => new StoreMapItemDto
            {
                Id = b.StoreEntity!.Id,
                Name = b.StoreEntity!.Name,
                Address = b.Address,
                Latitude = b.Latitude!.Value,
                Longitude = b.Longitude!.Value,
            })
            .ToListAsync(ct);
    }
}