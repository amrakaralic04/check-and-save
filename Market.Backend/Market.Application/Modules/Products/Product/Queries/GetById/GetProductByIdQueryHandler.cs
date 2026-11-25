namespace Market.Application.Modules.Products.Product.Queries.GetById
{
    public sealed class GetProductByIdQueryHandler(IAppDbContext ctx) : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryDto>
    {
        public async Task<GetProductByIdQueryDto> Handle(GetProductByIdQuery request, CancellationToken ct)
        {
            var product = await ctx.Products
                .Where(x => x.Id == request.Id)
                .Select(x => new GetProductByIdQueryDto
                {
                    Id = x.Id,
                    StoreEntityId = x.StoreEntityId,
                    BranchEntityId = x.BranchEntityId,
                    CategoryEntityId = x.CategoryEntityId,
                    BrandEntityId = x.BrandEntityId,
                    Name = x.Name,
                    Description = x.Description,
                    ImageURL = x.ImageURL,
                    DateAdded = x.DateAdded
                })
                .FirstOrDefaultAsync(ct);

            if (product is null)
                throw new MarketNotFoundException($"Product (ID={request.Id}) nije pronađen.");

            return product;
        }
    }
}
