namespace Market.Application.Modules.Products.Price.Queries.GetById
{
    public sealed class GetPriceByIdQueryHandler(IAppDbContext ctx) : IRequestHandler<GetPriceByIdQuery, GetPriceByIdQueryDto>
    {
        public async Task<GetPriceByIdQueryDto> Handle(GetPriceByIdQuery request, CancellationToken ct)
        {
            var price = await ctx.Prices
                .Where(x => x.Id == request.Id)
                .Select(x => new GetPriceByIdQueryDto
                {
                    Id = x.Id,
                    ProductEntityId = x.ProductEntityId,
                    Amount = x.Amount,
                    DateUpdated = x.DateUpdated
                })
                .FirstOrDefaultAsync(ct);

            if (price is null)
                throw new MarketNotFoundException($"Price (ID={request.Id}) nije pronađen.");

            return price;
        }
    }
}
