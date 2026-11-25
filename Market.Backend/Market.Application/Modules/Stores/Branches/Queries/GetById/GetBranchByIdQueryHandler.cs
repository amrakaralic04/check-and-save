namespace Market.Application.Modules.Stores.Branches.Queries.GetById;

public class GetBranchByIdQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetBranchByIdQuery, GetBranchByIdQueryDto>
{
    public async Task<GetBranchByIdQueryDto> Handle(GetBranchByIdQuery request, CancellationToken ct)
    {
        var branch = await ctx.Branches
            .AsNoTracking()
            .Where(b => b.Id == request.Id)
            .Select(x => new GetBranchByIdQueryDto
            {
                Id = x.Id,
                StoreEntityId = x.StoreEntityId,
                CityEntityId = x.CityEntityId,
                Address = x.Address,
                Contact = x.Contact,
                Email = x.Email,
                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync(ct);

        if (branch is null)
            throw new MarketNotFoundException($"Branch with Id {request.Id} not found.");

        return branch;
    }
}