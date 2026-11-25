namespace Market.Application.Modules.Products.Product.Queries.Compare
{
    public sealed class CompareProductsQueryDto
    {
        public IReadOnlyList<CompareProductDto> Products { get; init; } = [];
    }

    public sealed class CompareProductDto
    {
        public required string PublicId { get; init; }
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required string ImageURL { get; init; }
        public required DateTime DateAdded { get; init; }
        public required int CategoryEntityId { get; init; }
        public required string CategoryName { get; init; }
        public required int BrandEntityId { get; init; }
        public required string BrandName { get; init; }
        public IReadOnlyList<CompareStorePriceDto> Prices { get; init; } = [];
    }

    public sealed class CompareStorePriceDto
    {
        public required int ProductId { get; init; }
        public required string ProductPublicId { get; init; }
        public required int StoreEntityId { get; init; }
        public required string StoreName { get; init; }
        public required int BranchEntityId { get; init; }
        public required string BranchAddress { get; init; }
        public int? Amount { get; init; }
        public DateTime? DateUpdated { get; init; }
    }
}
