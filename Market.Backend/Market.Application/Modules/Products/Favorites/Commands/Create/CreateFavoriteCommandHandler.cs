using Market.Domain.Entities.ProductEntities;

namespace Market.Application.Modules.Products.Favorites.Commands.Create
{
     public sealed class CreateFavoriteCommandHandler(IAppDbContext ctx) : IRequestHandler<CreateFavoriteCommand, int>
    {
        public async Task<int> Handle(CreateFavoriteCommand request, CancellationToken ct)
        {
          
            var exists = await ctx.Favorites
                .AnyAsync(x => x.PublicUserEntityId == request.PublicUserEntityId
                            && x.ProductEntityId == request.ProductEntityId, ct);

            if (exists)
                throw new MarketConflictException("Favorite already exists.");

            var entity = new FavoritesEntity
            {
                PublicUserEntityId = request.PublicUserEntityId,
                ProductEntityId = request.ProductEntityId,
                DateAdded = request.DateAdded
            };

            await ctx.Favorites.AddAsync(entity, ct);
            await ctx.SaveChangesAsync(ct);

            return entity.Id;
        }
    }
}
