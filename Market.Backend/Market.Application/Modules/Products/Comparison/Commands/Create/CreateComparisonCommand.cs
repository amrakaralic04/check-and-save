namespace Market.Application.Modules.Products.Comparison.Commands.Create
{
    public sealed class CreateComparisonCommand : IRequest<int>
    {

        public int CustomerEntityId { get; set; }
        public DateTime Date { get; set; }
    }
}
