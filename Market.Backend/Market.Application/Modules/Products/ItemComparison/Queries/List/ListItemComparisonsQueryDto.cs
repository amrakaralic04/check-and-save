namespace Market.Application.Modules.Products.ItemComparison.Queries.List
{
    public sealed class ListItemComparisonsQueryDto
    {
        public int Id { get; set; }
        public int ComparisonEntityId { get; set; }
        public int ProductId { get; set; }
    }
}
