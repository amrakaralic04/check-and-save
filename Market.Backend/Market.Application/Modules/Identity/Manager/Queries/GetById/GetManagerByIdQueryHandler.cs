namespace Market.Application.Modules.Identity.Manager.Queries.GetById
{
    public sealed class GetManagerByIdQueryHandler(IAppDbContext ctx) : IRequestHandler<GetManagerByIdQuery, GetManagerByIdQueryDto>
    {
        public async Task<GetManagerByIdQueryDto> Handle(GetManagerByIdQuery request, CancellationToken ct)
        {
            var manager = await ctx.Managers
                .Where(x => x.Id == request.Id)
                .Select(x => new GetManagerByIdQueryDto
                {
                    Id = x.Id,
                    MarketUserEntityId = x.MarketUserEntityId,
                    StoreEntityId = x.StoreEntityId,
                    StartDate = x.StartDate
                })
                .FirstOrDefaultAsync(ct);

            if (manager is null)
                throw new MarketNotFoundException($"Manager (ID={request.Id}) nije pronađen.");

            return manager;
        }
    }
}
