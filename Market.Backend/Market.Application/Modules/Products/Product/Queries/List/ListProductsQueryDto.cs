namespace Market.Application.Modules.Products.Product.Queries.List
{
    public sealed class ListProductsQueryDto
    {
        public int Id { get; set; }
        public int BranchEntityId { get; set; }
        public int CategoryEntityId { get; set; }
        public int BrandEntityId { get; set; }
        public string Name { get; set; }
        public int StoreEntityId { get; set; }
        public string StoreLabel { get; set; }
        public decimal? LowestPrice { get; set; }
        public DateTime DateAdded { get; set; }
        public string? ImageURL { get; set; }
    }
}