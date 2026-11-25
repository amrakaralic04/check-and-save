namespace Market.Application.Modules.Products.Favorites.Queries.GetById
{
    public sealed class GetFavoriteByIdQuery : IRequest<GetFavoriteByIdQueryDto>
    {
        public int Id { get; set; }
    }
}
