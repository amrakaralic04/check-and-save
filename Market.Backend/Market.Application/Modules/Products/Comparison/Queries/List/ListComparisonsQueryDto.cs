namespace Market.Application.Modules.Products.Comparison.Queries.List
{
    public sealed class ListComparisonsQueryDto
    {
        public int Id { get; set; }
        public int CustomerEntityId { get; set; }
        public DateTime Date { get; set; }
    }
}
