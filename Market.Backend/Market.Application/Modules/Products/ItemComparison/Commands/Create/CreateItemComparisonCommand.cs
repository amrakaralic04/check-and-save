namespace Market.Application.Modules.Products.ItemComparison.Commands.Create
{
    public sealed class CreateItemComparisonCommand : IRequest<int>
    {
        public int ComparisonEntityId { get; set; }
        public int ProductId { get; set; }
    }
}
