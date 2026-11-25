namespace Market.Application.Modules.Analiytics.SaleStatistic.Queries.GetById
{
    public sealed class GetSalesStatisticByIdQueryDto
    {
        public int Id { get; init; }
        public int ManagerEntityId { get; init; }
        public int ProductEntityId { get; init; }

        public int ViewsCount { get; init; }
        public int SalesCount { get; init; }
        public DateTime Date { get; init; }

        public DateTime CreatedAt { get; init; }
        public DateTime? ModifiedAt { get; init; }
    }
}