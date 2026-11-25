namespace Market.Application.Modules.Identity.Manager.Queries.GetById
{
    public sealed class GetManagerByIdQueryDto
    {
        public required int Id { get; init; }
        public required int MarketUserEntityId { get; init; }
        public required int StoreEntityId { get; init; }
        public required DateTime StartDate { get; init; }
    }
}
