namespace Market.Application.Modules.Products.ItemComparison.Queries.GetById
{
    public sealed class GetItemComparisonByIdQueryHandler(IAppDbContext ctx) : IRequestHandler<GetItemComparisonByIdQuery, GetItemComparisonByIdQueryDto>
    {
        public async Task<GetItemComparisonByIdQueryDto> Handle(GetItemComparisonByIdQuery request, CancellationToken ct)
        {
            var item = await ctx.ItemComparisons
                .Where(x => x.Id == request.Id)
                .Select(x => new GetItemComparisonByIdQueryDto
                {
                    Id = x.Id,
                    ComparisonEntityId = x.ComparisonEntityId,
                    ProductId = x.ProductId
                })
                .FirstOrDefaultAsync(ct);

            if (item is null)
                throw new MarketNotFoundException($"ItemComparison (ID={request.Id}) nije pronađen.");

            return item;
        }
    }
}
