namespace Market.Application.Modules.Products.Favorites.Queries.List
{
    public sealed class ListFavoritesQueryDto
    {
        public int Id { get; set; }
        public string PublicId { get; set; } = string.Empty;
        public int PublicUserEntityId { get; set; }
        public int ProductEntityId { get; set; }
        public DateTime DateAdded { get; set; }

        public string? Name { get; set; }
        public int? Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
