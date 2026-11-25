namespace Market.Application.Modules.Products.Review.Queries.List
{
    public sealed class ListReviewsQueryHandler(IAppDbContext ctx) : IRequestHandler<ListReviewsQuery, PageResult<ListReviewsQueryDto>>
    {
        public async Task<PageResult<ListReviewsQueryDto>> Handle(ListReviewsQuery request, CancellationToken ct)
        {
            var q = ctx.Reviews.AsNoTracking();

            if (request.ProductEntityId.HasValue)
                q = q.Where(x => x.ProductEntityId == request.ProductEntityId.Value);

            if (request.PublicUserEntityId.HasValue)
                q = q.Where(x => x.PublicUserEntityId == request.PublicUserEntityId.Value);

            var pq = q.Select(x => new ListReviewsQueryDto
            {
                Id = x.Id,
                PublicUserEntityId = x.PublicUserEntityId,
                ProductEntityId = x.ProductEntityId,
                Rating = x.Rating,
                Comment = x.Comment,
                Date = x.Date
            });

            return await PageResult<ListReviewsQueryDto>
                .FromQueryableAsync(pq, request.Page, ct);
        }
    }
}
