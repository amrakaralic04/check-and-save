namespace Market.Application.Modules.Products.Product.Queries.Compare
{
    public sealed class CompareProductsQueryHandler(IAppDbContext ctx) : IRequestHandler<CompareProductsQuery, CompareProductsQueryDto>
    {
        public async Task<CompareProductsQueryDto> Handle(CompareProductsQuery request, CancellationToken ct)
        {
            var requestedIds = ParseRequestedIds(request.Ids);

            var seedProducts = await ctx.Products
                .AsNoTracking()
                .Where(x => requestedIds.Contains(x.Id))
                .Select(x => new ProductComparisonSeed
                {
                    Id = x.Id,
                    PublicId = x.Id.ToString(),
                    Name = x.Name,
                    Description = x.Description,
                    ImageURL = x.ImageURL,
                    DateAdded = x.DateAdded,
                    CategoryEntityId = x.CategoryEntityId,
                    CategoryName = x.CategoryEntity != null ? x.CategoryEntity.Name : string.Empty,
                    BrandEntityId = x.BrandEntityId,
                    BrandName = x.BrandEntity != null ? x.BrandEntity.Name : string.Empty
                })
                .ToListAsync(ct);

            var missingIds = requestedIds.Except(seedProducts.Select(x => x.Id)).ToArray();
            if (missingIds.Length > 0)
                throw new MarketNotFoundException($"Product publicId nije pronađen: {string.Join(", ", missingIds)}.");

            var comparedProducts = new List<CompareProductDto>();

            foreach (var seed in seedProducts.OrderBy(x => requestedIds.IndexOf(x.Id)))
            {
                var pricesAcrossStores = await ctx.Products
                    .AsNoTracking()
                    .Where(x => x.Name == seed.Name &&
                                x.CategoryEntityId == seed.CategoryEntityId &&
                                x.BrandEntityId == seed.BrandEntityId)
                    .Select(x => new CompareStorePriceDto
                    {
                        ProductId = x.Id,
                        ProductPublicId = x.Id.ToString(),
                        StoreEntityId = x.StoreEntityId,
                        StoreName = x.StoreEntity != null ? x.StoreEntity.Name : string.Empty,
                        BranchEntityId = x.BranchEntityId,
                        BranchAddress = x.BranchEntity != null ? x.BranchEntity.Address : string.Empty,
                        Amount = x.Prices
                            .OrderByDescending(p => p.DateUpdated)
                            .Select(p => (int?)p.Amount)
                            .FirstOrDefault(),
                        DateUpdated = x.Prices
                            .OrderByDescending(p => p.DateUpdated)
                            .Select(p => (DateTime?)p.DateUpdated)
                            .FirstOrDefault()
                    })
                    .OrderBy(x => x.Amount ?? int.MaxValue)
                    .ThenBy(x => x.StoreName)
                    .ThenBy(x => x.BranchAddress)
                    .ToListAsync(ct);

                comparedProducts.Add(new CompareProductDto
                {
                    PublicId = seed.PublicId,
                    Id = seed.Id,
                    Name = seed.Name,
                    Description = seed.Description,
                    ImageURL = seed.ImageURL,
                    DateAdded = seed.DateAdded,
                    CategoryEntityId = seed.CategoryEntityId,
                    CategoryName = seed.CategoryName,
                    BrandEntityId = seed.BrandEntityId,
                    BrandName = seed.BrandName,
                    Prices = pricesAcrossStores
                });
            }

            return new CompareProductsQueryDto
            {
                Products = comparedProducts
            };
        }

        private static List<int> ParseRequestedIds(string? ids)
        {
            return ids!
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .Distinct()
                .ToList();
        }

        private sealed class ProductComparisonSeed
        {
            public required int Id { get; init; }
            public required string PublicId { get; init; }
            public required string Name { get; init; }
            public required string Description { get; init; }
            public required string ImageURL { get; init; }
            public required DateTime DateAdded { get; init; }
            public required int CategoryEntityId { get; init; }
            public required string CategoryName { get; init; }
            public required int BrandEntityId { get; init; }
            public required string BrandName { get; init; }
        }
    }
}