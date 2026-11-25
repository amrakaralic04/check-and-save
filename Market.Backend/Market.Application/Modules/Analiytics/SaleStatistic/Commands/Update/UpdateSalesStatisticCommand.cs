namespace Market.Application.Modules.Analiytics.SaleStatistic.Commands.Update
{
    public sealed class UpdateSalesStatisticCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public int ViewsCount { get; set; }
        public int SalesCount { get; set; }
        public DateTime Date { get; set; }
    }
}