namespace Market.Application.Modules.Analiytics.SaleStatistic.Queries.List
{
    public sealed class ListSalesStatisticsQueryDto
    {
        public int Id { get; init; }
        public int ManagerEntityId { get; init; }
        public int ProductEntityId { get; init; }
        public int ViewsCount { get; init; }
        public int SalesCount { get; init; }
        public DateTime Date { get; init; }
    }
}