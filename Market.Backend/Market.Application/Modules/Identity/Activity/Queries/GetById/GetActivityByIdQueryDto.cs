namespace Market.Application.Modules.Identity.Activity.Queries.GetById
{
    public sealed class GetActivityByIdQueryDto
    {
        public required int Id { get; init; }
        public required int MarketUserEntityId { get; init; }
        public required string ActivityType { get; init; }
        public required string Description { get; init; }
        public required DateTime Date { get; init; }
    }
}
