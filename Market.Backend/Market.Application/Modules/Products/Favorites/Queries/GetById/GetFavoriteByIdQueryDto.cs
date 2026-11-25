namespace Market.Application.Modules.Products.Favorites.Queries.GetById
{
    public sealed class GetFavoriteByIdQueryDto
    {
        public required int Id { get; init; }
        public required int PublicUserEntityId { get; init; }
        public required int ProductEntityId { get; init; }
        public required DateTime DateAdded { get; init; }
    }
}
