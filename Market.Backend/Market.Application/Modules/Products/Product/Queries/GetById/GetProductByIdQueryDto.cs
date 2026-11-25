namespace Market.Application.Modules.Products.Product.Queries.GetById
{
    public sealed class GetProductByIdQueryDto
    {
        public required int Id { get; init; }
        public required int StoreEntityId { get; init; }
        public required int BranchEntityId { get; init; }
        public required int CategoryEntityId { get; init; }
        public required int BrandEntityId { get; init; }

        public required string Name { get; init; }
        public required string Description { get; init; }
        public required string ImageURL { get; init; }
        public required DateTime DateAdded { get; init; }
    }
}
