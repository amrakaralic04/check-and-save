namespace Market.Application.Modules.Products.Favorites.Queries.List
{
    public sealed class ListFavoritesQuery : BasePagedQuery<ListFavoritesQueryDto>
    {
        public int? PublicUserEntityId { get; set; }
       
    }
}
