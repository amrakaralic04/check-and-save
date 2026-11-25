namespace Market.Application.Modules.Analiytics.Report.Queries.GetById
{
    public sealed class GetReportByIdQuery : IRequest<GetReportByIdQueryDto>
    {
        public int Id { get; set; }
    }
}