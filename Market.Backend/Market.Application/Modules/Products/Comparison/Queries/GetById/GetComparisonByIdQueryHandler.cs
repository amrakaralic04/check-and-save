namespace Market.Application.Modules.Products.Comparison.Queries.GetById
{
    public sealed class GetComparisonByIdQueryHandler(IAppDbContext ctx): IRequestHandler<GetComparisonByIdQuery, GetComparisonByIdQueryDto>
    {
        public async Task<GetComparisonByIdQueryDto> Handle(
        GetComparisonByIdQuery request,
        CancellationToken ct)
        {
            var comparison = await ctx.Comparisons
                .Where(x => x.Id == request.Id)
                .Select(x => new GetComparisonByIdQueryDto
                {
                    Id = x.Id,
                    CustomerEntityId = x.CustomerEntityId,
                    Date = x.Date
                })
                .FirstOrDefaultAsync(ct);

            if (comparison is null)
                throw new MarketNotFoundException(
                    $"Comparison (ID={request.Id}) nije pronađen.");

            return comparison;
        }
    }
}
