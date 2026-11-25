namespace Market.Application.Modules.Products.Favorites.Commands.Create
{
    public sealed class CreateFavoriteCommand : IRequest<int>
    {
        public int PublicUserEntityId { get; set; }
        public int ProductEntityId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
