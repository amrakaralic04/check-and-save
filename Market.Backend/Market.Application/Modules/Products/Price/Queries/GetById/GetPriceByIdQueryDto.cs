namespace Market.Application.Modules.Products.Price.Queries.GetById
{
    public sealed class GetPriceByIdQueryDto
    {
        public required int Id { get; init; }
        public required int ProductEntityId { get; init; }
        public required int Amount { get; init; }
        public required DateTime DateUpdated { get; init; }
    }
}
