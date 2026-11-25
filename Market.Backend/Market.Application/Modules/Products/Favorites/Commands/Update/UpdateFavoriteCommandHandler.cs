namespace Market.Application.Modules.Products.Favorites.Commands.Update
{
    public sealed class UpdateFavoriteCommandHandler(IAppDbContext ctx) : IRequestHandler<UpdateFavoriteCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateFavoriteCommand request, CancellationToken ct)
        {
            var entity = await ctx.Favorites
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Favorite (ID={request.Id}) nije pronađen.");

            entity.DateAdded = request.DateAdded;

            await ctx.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
