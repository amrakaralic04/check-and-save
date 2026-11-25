namespace Market.Application.Modules.Products.Favorites.Commands.Delete
{
    public sealed class DeleteFavoriteCommandHandler(IAppDbContext ctx) : IRequestHandler<DeleteFavoriteCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteFavoriteCommand request, CancellationToken ct)
        {
            var entity = await ctx.Favorites
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Favorite (ID={request.Id}) nije pronađen.");

            ctx.Favorites.Remove(entity);
            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
