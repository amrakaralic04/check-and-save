namespace Market.Application.Modules.Analiytics.Report.Commands.Create
{
    public sealed class CreateReportCommand : IRequest<int>
    {
        public int MarketUserEntityId { get; set; }
        public string ReportType { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime ReportDate { get; set; }
    }
}