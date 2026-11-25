namespace Market.Application.Modules.Products.ItemComparison.Queries.List
{
    public sealed class ListItemComparisonsQueryHandler(IAppDbContext ctx) : IRequestHandler<ListItemComparisonsQuery, PageResult<ListItemComparisonsQueryDto>>
    {
        public async Task<PageResult<ListItemComparisonsQueryDto>> Handle(ListItemComparisonsQuery request, CancellationToken ct)
        {
            var q = ctx.ItemComparisons.AsNoTracking();

            if (request.ComparisonEntityId.HasValue)
            {
                q = q.Where(x => x.ComparisonEntityId == request.ComparisonEntityId.Value);
            }

            var pq = q.Select(x => new ListItemComparisonsQueryDto
            {
                Id = x.Id,
                ComparisonEntityId = x.ComparisonEntityId,
                ProductId = x.ProductId
            });

            return await PageResult<ListItemComparisonsQueryDto>
                .FromQueryableAsync(pq, request.Page, ct);
        }
    }
}
