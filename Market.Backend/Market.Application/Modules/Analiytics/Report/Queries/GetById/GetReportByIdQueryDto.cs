namespace Market.Application.Modules.Analiytics.Report.Queries.GetById
{
    public sealed class GetReportByIdQueryDto
    {
        public int Id { get; init; }
        public int MarketUserEntityId { get; init; }
        public string ReportType { get; init; } = default!;
        public string? Description { get; init; }
        public DateTime ReportDate { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? ModifiedAt { get; init; }
    }
}