namespace Market.Application.Modules.Analiytics.Report.Commands.Delete
{
    public sealed class DeleteReportCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}