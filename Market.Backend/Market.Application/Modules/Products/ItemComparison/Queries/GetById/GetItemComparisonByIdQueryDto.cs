namespace Market.Application.Modules.Products.ItemComparison.Queries.GetById
{
    public sealed class GetItemComparisonByIdQueryDto
    {
        public required int Id { get; init; }
        public required int ComparisonEntityId { get; init; }
        public required int ProductId { get; init; }
    }
}
