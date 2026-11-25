namespace Market.Application.Modules.Products.Review.Commands.Create
{
    public sealed class CreateReviewCommand : IRequest<int>
    {
        public int PublicUserEntityId { get; set; }
        public int ProductEntityId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
