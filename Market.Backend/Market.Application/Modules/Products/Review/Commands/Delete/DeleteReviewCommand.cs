namespace Market.Application.Modules.Products.Review.Commands.Delete
{
    public sealed class DeleteReviewCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
