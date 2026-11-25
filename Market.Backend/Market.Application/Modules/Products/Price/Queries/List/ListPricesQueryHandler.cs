namespace Market.Application.Modules.Products.Price.Queries.List
{
    public sealed class ListPricesQueryHandler(IAppDbContext ctx) : IRequestHandler<ListPricesQuery, PageResult<ListPricesQueryDto>>
    {
        public async Task<PageResult<ListPricesQueryDto>> Handle(ListPricesQuery request, CancellationToken ct)
        {
            var q = ctx.Prices.AsNoTracking();

            if (request.ProductEntityId.HasValue)
            {
                q = q.Where(x => x.ProductEntityId == request.ProductEntityId.Value);
            }

            var pq = q.Select(x => new ListPricesQueryDto
            {
                Id = x.Id,
                ProductEntityId = x.ProductEntityId,
                Amount = x.Amount,
                DateUpdated = x.DateUpdated
            });

            return await PageResult<ListPricesQueryDto>
                .FromQueryableAsync(pq, request.Page, ct);
        }
    }
}
