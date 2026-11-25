using Market.Domain.Entities.Analytics;

namespace Market.Application.Modules.Analiytics.SaleStatistic.Queries.List
{
    public sealed class ListSalesStatisticsQueryHandler(IAppDbContext ctx)
    : IRequestHandler<ListSalesStatisticsQuery, PageResult<ListSalesStatisticsQueryDto>>
    {
        public async Task<PageResult<ListSalesStatisticsQueryDto>> Handle(ListSalesStatisticsQuery request, CancellationToken ct)
        {
            var q = ctx.SaleStatistics
                .AsNoTracking()
                .Where(x => !x.IsDeleted);

            if (request.ManagerEntityId is not null)
                q = q.Where(x => x.ManagerEntityId == request.ManagerEntityId);

            if (request.ProductEntityId is not null)
                q = q.Where(x => x.ProductEntityId == request.ProductEntityId);

            if (request.DateFrom is not null)
                q = q.Where(x => x.Date >= request.DateFrom);

            if (request.DateTo is not null)
                q = q.Where(x => x.Date <= request.DateTo);

            var projected = q
                .OrderByDescending(x => x.Date)
                .Select(x => new ListSalesStatisticsQueryDto
                {
                    Id = x.Id,
                    ManagerEntityId = x.ManagerEntityId,
                    ProductEntityId = x.ProductEntityId,
                    ViewsCount = x.ViewsCount,
                    SalesCount = x.SalesCount,
                    Date = x.Date
                });

            return await PageResult<ListSalesStatisticsQueryDto>.FromQueryableAsync(projected, request.Paging, ct);
        }
    }
}