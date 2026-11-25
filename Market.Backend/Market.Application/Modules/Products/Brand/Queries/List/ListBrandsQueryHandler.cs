namespace Market.Application.Modules.Products.Brand.Queries.List
{
    public sealed class ListBrandsQueryHandler(IAppDbContext context) : IRequestHandler<ListBrandsQuery, PageResult<ListBrandsQueryDto>>
    {
        public async Task<PageResult<ListBrandsQueryDto>> Handle(ListBrandsQuery request, CancellationToken cancellationToken)
        {
            var q = context.Brands.AsNoTracking();

            if (!string.IsNullOrEmpty(request.Search))
            {
                q = q.Where(x => x.Name.ToLower().StartsWith(request.Search.ToLower()));
            }

            var pq = q.Select(x => new ListBrandsQueryDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            });

            return await PageResult<ListBrandsQueryDto>
                .FromQueryableAsync(pq, request.Paging, cancellationToken);
        }
    }
}

