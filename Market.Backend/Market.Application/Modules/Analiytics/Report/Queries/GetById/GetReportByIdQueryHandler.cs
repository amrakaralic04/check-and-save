namespace Market.Application.Modules.Analiytics.Report.Queries.GetById
{
    public sealed class GetReportByIdQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetReportByIdQuery, GetReportByIdQueryDto>
    {
        public async Task<GetReportByIdQueryDto> Handle(GetReportByIdQuery request, CancellationToken ct)
        {
            var dto = await ctx.Reports
                .AsNoTracking()
                .Where(x => x.Id == request.Id && !x.IsDeleted)
                .Select(x => new GetReportByIdQueryDto
                {
                    Id = x.Id,
                    MarketUserEntityId = x.MarketUserEntityId,
                    ReportType = x.ReportType,
                    Description = x.Description,
                    ReportDate = x.ReportDate,
                    CreatedAt = x.CreatedAt,
                    ModifiedAt = x.ModifiedAt
                })
                .FirstOrDefaultAsync(ct);

            if (dto is null)
                throw new MarketNotFoundException($"Report with Id {request.Id} not found.");

            return dto;
        }
    }
}