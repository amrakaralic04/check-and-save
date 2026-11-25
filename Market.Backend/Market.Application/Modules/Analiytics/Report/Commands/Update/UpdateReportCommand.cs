namespace Market.Application.Modules.Analiytics.Report.Commands.Update
{
    public sealed class UpdateReportCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string ReportType { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime ReportDate { get; set; }
    }
}