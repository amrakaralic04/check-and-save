namespace Market.Application.Modules.Products.Category.Commands.Update
{
    public sealed class UpdateCategoryCommandHandler(IAppDbContext ctx) : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken ct)
        {
            var entity = await ctx.Categories
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Kategorija (ID={request.Id}) nije pronađena.");

            var exists = await ctx.Categories
                .AnyAsync(x => x.Id != request.Id && x.Name.ToLower() == request.Name.ToLower(), ct);

            if (exists)
                throw new MarketConflictException("Name already exists.");

            entity.Name = request.Name.Trim();
            entity.Description = request.Description?.Trim();

            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
