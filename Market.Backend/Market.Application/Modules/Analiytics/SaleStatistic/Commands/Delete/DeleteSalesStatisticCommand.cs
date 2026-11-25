namespace Market.Application.Modules.Analiytics.SaleStatistic.Commands.Delete
{
    public sealed class DeleteSalesStatisticCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}