namespace Market.Application.Modules.Analiytics.SaleStatistic.Queries.List
{
    public sealed class ListSalesStatisticsQuery : BasePagedQuery<ListSalesStatisticsQueryDto>
    {
        public int? ManagerEntityId { get; init; }
        public int? ProductEntityId { get; init; }
        public DateTime? DateFrom { get; init; }
        public DateTime? DateTo { get; init; }
    }
}