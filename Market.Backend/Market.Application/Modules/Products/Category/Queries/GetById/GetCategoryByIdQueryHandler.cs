namespace Market.Application.Modules.Products.Category.Queries.GetById
{
    public sealed class GetCategoryByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdQueryDto>
    {
        public async Task<GetCategoryByIdQueryDto> Handle(GetCategoryByIdQuery request, CancellationToken ct)
        {
            var category = await context.Categories
                .Where(x => x.Id == request.Id)
                .Select(x => new GetCategoryByIdQueryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .FirstOrDefaultAsync(ct);

            if (category is null)
                throw new MarketNotFoundException($"Kategorija (ID={request.Id}) nije pronađena.");

            return category;
        }
    }
}
