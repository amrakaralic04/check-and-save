namespace Market.Application.Modules.Products.Comparison.Queries.GetById
{
    public sealed class GetComparisonByIdQueryDto
    {
        public required int Id { get; init; }
        public required int CustomerEntityId { get; init; }
        public required DateTime Date { get; init; }
    }
}
