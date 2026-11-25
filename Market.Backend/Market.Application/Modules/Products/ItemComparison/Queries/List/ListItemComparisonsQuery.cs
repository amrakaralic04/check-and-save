namespace Market.Application.Modules.Products.ItemComparison.Queries.List
{
    public sealed class ListItemComparisonsQuery : BasePagedQuery<ListItemComparisonsQueryDto>
    {
        public int? ComparisonEntityId { get; set; }
        public PageRequest Page { get; internal set; }
    }
}
