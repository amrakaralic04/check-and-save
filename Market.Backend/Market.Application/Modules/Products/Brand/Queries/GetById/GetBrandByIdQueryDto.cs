namespace Market.Application.Modules.Products.Brand.Queries.GetById
{
    public sealed class GetBrandByIdQueryDto
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public string? Description { get; init; }
    }
}
