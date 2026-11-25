namespace Market.Application.Modules.Analiytics.Report.Queries.List
{
    public sealed class ListReportsQueryDto
    {
        public int Id { get; init; }
        public int MarketUserEntityId { get; init; }
        public string ReportType { get; init; } = default!;
        public string? Description { get; init; }
        public DateTime ReportDate { get; init; }
    }
}