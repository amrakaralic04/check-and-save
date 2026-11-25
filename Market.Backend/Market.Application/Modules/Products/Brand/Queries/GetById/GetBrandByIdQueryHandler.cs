namespace Market.Application.Modules.Products.Brand.Queries.GetById
{
    public sealed class GetBrandByIdQueryHandler(IAppDbContext ctx): IRequestHandler<GetBrandByIdQuery, GetBrandByIdQueryDto>
    {
        public async Task<GetBrandByIdQueryDto> Handle(GetBrandByIdQuery request, CancellationToken ct)
        {
            var brand = await ctx.Brands
                .Where(x => x.Id == request.Id)
                .Select(x => new GetBrandByIdQueryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                    // IsEnabled = x.IsEnabled
                })
                .FirstOrDefaultAsync(ct);

            if (brand is null)
                throw new MarketNotFoundException($"Brend (ID={request.Id}) nije pronađen.");

            return brand;
        }
    }
}
