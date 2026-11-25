namespace Market.Application.Modules.Products.Review.Queries.GetById
{
    public sealed class GetReviewByIdQuery : IRequest<GetReviewByIdQueryDto>
    {
        public int Id { get; set; }
    }
}
