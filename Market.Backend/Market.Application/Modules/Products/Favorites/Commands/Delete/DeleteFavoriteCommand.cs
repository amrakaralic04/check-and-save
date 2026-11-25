namespace Market.Application.Modules.Products.Favorites.Commands.Delete
{
    public sealed class DeleteFavoriteCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
