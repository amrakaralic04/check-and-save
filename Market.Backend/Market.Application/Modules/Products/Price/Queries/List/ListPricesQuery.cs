namespace Market.Application.Modules.Products.Price.Queries.List
{
    public sealed class ListPricesQuery : BasePagedQuery<ListPricesQueryDto>
    {
        public int? ProductEntityId { get; set; }
        public PageRequest Page { get; internal set; }
    }
}
