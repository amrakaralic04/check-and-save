namespace Market.Application.Modules.Analiytics.SaleStatistic.Queries.GetById
{
    public sealed class GetSalesStatisticByIdQuery : IRequest<GetSalesStatisticByIdQueryDto>
    {
        public int Id { get; set; }
    }
}