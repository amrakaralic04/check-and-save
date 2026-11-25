namespace Market.Application.Modules.Products.Brand.Commands.Update
{
    public sealed class UpdateBrandCommandHandler(IAppDbContext ctx) : IRequestHandler<UpdateBrandCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateBrandCommand request, CancellationToken ct)
        {
            var entity = await ctx.Brands
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Brend (ID={request.Id}) nije pronađen.");

            var exists = await ctx.Brands
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
