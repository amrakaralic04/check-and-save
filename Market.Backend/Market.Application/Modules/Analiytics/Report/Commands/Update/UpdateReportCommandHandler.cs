namespace Market.Application.Modules.Analiytics.Report.Commands.Update
{
    public sealed class UpdateReportCommandHandler(IAppDbContext ctx)
    : IRequestHandler<UpdateReportCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateReportCommand request, CancellationToken ct)
        {
            var entity = await ctx.Reports
                .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, ct);

            if (entity is null)
                throw new MarketNotFoundException($"Report (ID={request.Id}) not found.");

            var reportType = request.ReportType?.Trim();
            if (string.IsNullOrWhiteSpace(reportType))
                throw new ValidationException("ReportType is required.");

            bool exists = await ctx.Reports
                .AnyAsync(x => !x.IsDeleted
                               && x.Id != request.Id
                               && x.MarketUserEntityId == entity.MarketUserEntityId
                               && x.ReportType == reportType
                               && x.ReportDate.Date == request.ReportDate.Date, ct);

            if (exists)
                throw new MarketConflictException("Report with the same type and date already exists for this user.");

            entity.ReportType = reportType;
            entity.Description = request.Description?.Trim();
            entity.ReportDate = request.ReportDate;
            entity.ModifiedAt = DateTime.UtcNow;

            await ctx.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}