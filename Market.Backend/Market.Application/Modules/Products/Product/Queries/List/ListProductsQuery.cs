namespace Market.Application.Modules.Products.Product.Queries.List
{
    public sealed class ListProductsQuery : BasePagedQuery<ListProductsQueryDto>
    {
        public string? Search { get; set; }
        public int? BranchEntityId { get; set; }
        public int? CategoryEntityId { get; set; }
        public int? BrandEntityId { get; set; }
        public int? StoreEntityId { get; set; }
    }
}