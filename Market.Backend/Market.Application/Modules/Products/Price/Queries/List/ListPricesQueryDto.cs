namespace Market.Application.Modules.Products.Price.Queries.List
{
    public sealed class ListPricesQueryDto
    {
        public int Id { get; set; }
        public int ProductEntityId { get; set; }
        public int Amount { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
