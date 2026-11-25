namespace Market.Application.Modules.Products.Review.Queries.GetById
{
    public sealed class GetReviewByIdQueryHandler(IAppDbContext ctx) : IRequestHandler<GetReviewByIdQuery, GetReviewByIdQueryDto>
    {
        public async Task<GetReviewByIdQueryDto> Handle(GetReviewByIdQuery request, CancellationToken ct)
        {
            var review = await ctx.Reviews
                .Where(x => x.Id == request.Id)
                .Select(x => new GetReviewByIdQueryDto
                {
                    Id = x.Id,
                    PublicUserEntityId = x.PublicUserEntityId,
                    ProductEntityId = x.ProductEntityId,
                    Rating = x.Rating,
                    Comment = x.Comment,
                    Date = x.Date
                })
                .FirstOrDefaultAsync(ct);

            if (review is null)
                throw new MarketNotFoundException($"Review (ID={request.Id}) nije pronađen.");

            return review;
        }
    }
}
