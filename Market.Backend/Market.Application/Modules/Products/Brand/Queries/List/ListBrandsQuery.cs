namespace Market.Application.Modules.Products.Brand.Queries.List
{
    public sealed class ListBrandsQuery : BasePagedQuery<ListBrandsQueryDto>
    {
        public string? Search { get; set; }
    }
}
