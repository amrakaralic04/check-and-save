namespace Market.Application.Modules.Products.Review.Queries.GetById
{
    public sealed class GetReviewByIdQueryDto
    {
        public required int Id { get; init; }
        public required int PublicUserEntityId { get; init; }
        public required int ProductEntityId { get; init; }
        public required int Rating { get; init; }
        public required string? Comment { get; init; }
        public required DateTime Date { get; init; }
    }
}
