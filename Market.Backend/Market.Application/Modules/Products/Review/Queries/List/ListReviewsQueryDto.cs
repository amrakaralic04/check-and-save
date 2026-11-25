namespace Market.Application.Modules.Products.Review.Queries.List
{
    public sealed class ListReviewsQueryDto
    {
        public int Id { get; set; }
        public int PublicUserEntityId { get; set; }
        public int ProductEntityId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
