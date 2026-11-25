namespace Market.Application.Modules.Products.Favorites.Queries.GetById
{
    public sealed class GetFavoriteByIdQueryHandler(IAppDbContext ctx) : IRequestHandler<GetFavoriteByIdQuery, GetFavoriteByIdQueryDto>
    {
        public async Task<GetFavoriteByIdQueryDto> Handle(GetFavoriteByIdQuery request, CancellationToken ct)
        {
            var fav = await ctx.Favorites
                .Where(x => x.Id == request.Id)
                .Select(x => new GetFavoriteByIdQueryDto
                {
                    Id = x.Id,
                    PublicUserEntityId = x.PublicUserEntityId,
                    ProductEntityId = x.ProductEntityId,
                    DateAdded = x.DateAdded
                })
                .FirstOrDefaultAsync(ct);

            if (fav is null)
                throw new MarketNotFoundException($"Favorite (ID={request.Id}) nije pronađen.");

            return fav;
        }
    }
}
