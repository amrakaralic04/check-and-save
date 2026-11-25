using Market.Domain.Entities.Analytics;

namespace Market.Application.Modules.Analiytics.Report.Commands.Create
{
    public sealed class CreateReportCommandHandler(IAppDbContext context)
    : IRequestHandler<CreateReportCommand, int>
    {
        public async Task<int> Handle(CreateReportCommand request, CancellationToken ct)
        {
            var reportType = request.ReportType?.Trim();

            if (string.IsNullOrWhiteSpace(reportType))
                throw new ValidationException("ReportType is required.");

            bool exists = await context.Reports
                .AnyAsync(x => !x.IsDeleted
                               && x.MarketUserEntityId == request.MarketUserEntityId
                               && x.ReportType == reportType
                               && x.ReportDate.Date == request.ReportDate.Date, ct);

            if (exists)
                throw new MarketConflictException("Report with the same type and date already exists for this user.");

            var entity = new ReportEntity
            {
                MarketUserEntityId = request.MarketUserEntityId,
                ReportType = reportType,
                Description = request.Description?.Trim(),
                ReportDate = request.ReportDate,

                CreatedAt = DateTime.UtcNow,
                ModifiedAt = null,
                IsDeleted = false
            };

            context.Reports.Add(entity);
            await context.SaveChangesAsync(ct);

            return entity.Id;
        }
    }
}