namespace Market.Application.Modules.Store.Stores.Queries.GetById;

public class GetStoreByIdQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetStoreByIdQuery, GetStoreByIdQueryDto>
{
    public async Task<GetStoreByIdQueryDto> Handle(GetStoreByIdQuery request, CancellationToken ct)
    {
        var store = await ctx.Stores
            .AsNoTracking()
            .Where(s => s.Id == request.Id)
            .Select(x => new GetStoreByIdQueryDto
            {
                Id = x.Id,
                Name = x.Name,
                Contact = x.Contact,
                Email = x.Email,
                IsActive = x.IsActive,
                CityEntityId = x.CityEntityId
            })
            .FirstOrDefaultAsync(ct);

        if (store is null)
            throw new MarketNotFoundException($"Store with Id {request.Id} not found.");

        return store;
    }
}