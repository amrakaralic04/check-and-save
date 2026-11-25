namespace Market.Application.Modules.Stores.Branches.Queries.Map;

public sealed class GetBranchesMapQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetBranchesMapQuery, List<BranchMapItemDto>>
{
    public async Task<List<BranchMapItemDto>> Handle(
        GetBranchesMapQuery request,
        CancellationToken ct)
    {
        return await ctx.Branches
            .AsNoTracking()
            .Where(b => !b.IsDeleted
                        && b.IsActive
                        && b.Latitude.HasValue
                        && b.Longitude.HasValue)
            .Include(b => b.StoreEntity)
            .Select(b => new BranchMapItemDto
            {
                Id = b.Id,
                StoreName = b.StoreEntity!.Name,
                Address = b.Address,
                Contact = b.Contact,
                Email = b.Email,
                Latitude = b.Latitude!.Value,
                Longitude = b.Longitude!.Value,
            })
            .ToListAsync(ct);
    }
}