namespace Market.Application.Modules.Products.Category.Commands.Delete
{
    public sealed class DeleteCategoryCommandHandler(IAppDbContext ctx)
    : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken ct)
        {
            var entity = await ctx.Categories
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Kategorija (ID={request.Id}) nije pronađena.");

            ctx.Categories.Remove(entity);
            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
