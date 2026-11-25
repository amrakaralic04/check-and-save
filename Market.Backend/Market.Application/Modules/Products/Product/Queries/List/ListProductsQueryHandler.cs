namespace Market.Application.Modules.Products.Product.Queries.List
{
    public sealed class ListProductsQueryHandler(IAppDbContext ctx) : IRequestHandler<ListProductsQuery, PageResult<ListProductsQueryDto>>
    {
        public async Task<PageResult<ListProductsQueryDto>> Handle(ListProductsQuery request, CancellationToken ct)
        {
            var q = ctx.Products.AsNoTracking();

            if (!string.IsNullOrEmpty(request.Search))
            {
                var s = request.Search.ToLower();
                q = q.Where(x => x.Name.ToLower().StartsWith(s));
            }

            if (request.BranchEntityId.HasValue)
                q = q.Where(x => x.BranchEntityId == request.BranchEntityId.Value);

            if (request.CategoryEntityId.HasValue)
                q = q.Where(x => x.CategoryEntityId == request.CategoryEntityId.Value);

            if (request.BrandEntityId.HasValue)
                q = q.Where(x => x.BrandEntityId == request.BrandEntityId.Value);

            if (request.StoreEntityId.HasValue)
                q = q.Where(x => x.StoreEntityId == request.StoreEntityId.Value);

            var pq = q.Select(x => new ListProductsQueryDto
            {
                Id = x.Id,
                StoreEntityId = x.StoreEntityId,
                BranchEntityId = x.BranchEntityId,
                CategoryEntityId = x.CategoryEntityId,
                BrandEntityId = x.BrandEntityId,
                Name = x.Name,
                StoreLabel = x.StoreEntity != null ? x.StoreEntity.Name : string.Empty,
                LowestPrice = x.Prices.Select(p => (int?)p.Amount).Min(),
                DateAdded = x.DateAdded,
                ImageURL = x.ImageURL
            });

            return await PageResult<ListProductsQueryDto>.FromQueryableAsync(pq, request.Paging, ct);
        }
    }
}