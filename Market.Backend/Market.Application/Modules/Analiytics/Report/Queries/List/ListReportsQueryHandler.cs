namespace Market.Application.Modules.Analiytics.Report.Queries.List
{
    public sealed class ListReportsQueryHandler(IAppDbContext ctx)
    : IRequestHandler<ListReportsQuery, PageResult<ListReportsQueryDto>>
    {
        public async Task<PageResult<ListReportsQueryDto>> Handle(ListReportsQuery request, CancellationToken ct)
        {
            var q = ctx.Reports
                .AsNoTracking()
                .Where(x => !x.IsDeleted);

            if (request.MarketUserEntityId is not null)
                q = q.Where(x => x.MarketUserEntityId == request.MarketUserEntityId);

            if (!string.IsNullOrWhiteSpace(request.ReportType))
            {
                var type = request.ReportType.Trim();
                q = q.Where(x => x.ReportType.Contains(type));
            }

            if (request.DateFrom is not null)
                q = q.Where(x => x.ReportDate >= request.DateFrom);

            if (request.DateTo is not null)
                q = q.Where(x => x.ReportDate <= request.DateTo);

            var projected = q
                .OrderByDescending(x => x.ReportDate)
                .Select(x => new ListReportsQueryDto
                {
                    Id = x.Id,
                    MarketUserEntityId = x.MarketUserEntityId,
                    ReportType = x.ReportType,
                    Description = x.Description,
                    ReportDate = x.ReportDate
                });

            return await PageResult<ListReportsQueryDto>.FromQueryableAsync(projected, request.Paging, ct);
        }
    }
}