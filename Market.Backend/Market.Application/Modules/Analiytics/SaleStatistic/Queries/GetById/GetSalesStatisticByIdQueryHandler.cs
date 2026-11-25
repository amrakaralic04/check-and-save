namespace Market.Application.Modules.Analiytics.SaleStatistic.Queries.GetById
{
    public sealed class GetSalesStatisticByIdQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetSalesStatisticByIdQuery, GetSalesStatisticByIdQueryDto>
    {
        public async Task<GetSalesStatisticByIdQueryDto> Handle(GetSalesStatisticByIdQuery request, CancellationToken ct)
        {
            var dto = await ctx.SaleStatistics
                .AsNoTracking()
                .Where(x => x.Id == request.Id && !x.IsDeleted)
                .Select(x => new GetSalesStatisticByIdQueryDto
                {
                    Id = x.Id,
                    ManagerEntityId = x.ManagerEntityId,
                    ProductEntityId = x.ProductEntityId,
                    ViewsCount = x.ViewsCount,
                    SalesCount = x.SalesCount,
                    Date = x.Date,
                    CreatedAt = x.CreatedAt,
                    ModifiedAt = x.ModifiedAt
                })
                .FirstOrDefaultAsync(ct);

            if (dto is null)
                throw new MarketNotFoundException($"SalesStatistic with Id {request.Id} not found.");

            return dto;
        }
    }
}