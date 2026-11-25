namespace Market.Application.Modules.Analiytics.SaleStatistic.Commands.Create
{
    public sealed class CreateSalesStatisticCommand : IRequest<int>
    {
        public int ManagerEntityId { get; set; }
        public int ProductEntityId { get; set; }

        public int ViewsCount { get; set; }
        public int SalesCount { get; set; }
        public DateTime Date { get; set; }
    }
}