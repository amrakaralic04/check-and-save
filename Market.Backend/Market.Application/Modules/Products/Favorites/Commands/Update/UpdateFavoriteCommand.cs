namespace Market.Application.Modules.Products.Favorites.Commands.Update
{
    public sealed class UpdateFavoriteCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
