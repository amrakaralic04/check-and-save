namespace Market.Application.Modules.Products.Product.Queries.Compare
{
    public sealed class CompareProductsQuery : IRequest<CompareProductsQueryDto>
    {
        public string? Ids { get; set; }
    }
}