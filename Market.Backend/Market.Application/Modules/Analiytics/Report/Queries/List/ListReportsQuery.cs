namespace Market.Application.Modules.Analiytics.Report.Queries.List
{
    public sealed class ListReportsQuery : BasePagedQuery<ListReportsQueryDto>
    {
        public int? MarketUserEntityId { get; init; }
        public string? ReportType { get; init; }
        public DateTime? DateFrom { get; init; }
        public DateTime? DateTo { get; init; }
    }
}