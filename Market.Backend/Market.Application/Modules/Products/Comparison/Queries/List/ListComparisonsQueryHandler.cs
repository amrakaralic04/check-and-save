namespace Market.Application.Modules.Products.Comparison.Queries.List
{
    public sealed class ListComparisonsQueryHandler(IAppDbContext ctx): IRequestHandler<ListComparisonsQuery, PageResult<ListComparisonsQueryDto>>
    {
        public async Task<PageResult<ListComparisonsQueryDto>> Handle(
        ListComparisonsQuery request,
        CancellationToken ct)
        {
            var q = ctx.Comparisons.AsNoTracking();

            if (request.CustomerEntityId.HasValue)
            {
                q = q.Where(x => x.CustomerEntityId == request.CustomerEntityId.Value);
            }

            var pq = q.Select(x => new ListComparisonsQueryDto
            {
                Id = x.Id,
                CustomerEntityId = x.CustomerEntityId,
                Date = x.Date
            });

            return await PageResult<ListComparisonsQueryDto>
                .FromQueryableAsync(pq, request.Page, ct);
        }
    }
}
