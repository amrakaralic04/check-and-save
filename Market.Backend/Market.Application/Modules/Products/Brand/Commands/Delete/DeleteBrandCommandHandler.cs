namespace Market.Application.Modules.Products.Brand.Commands.Delete
{
    public sealed class DeleteBrandCommandHandler(IAppDbContext ctx) : IRequestHandler<DeleteBrandCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteBrandCommand request, CancellationToken ct)
        {
            var entity = await ctx.Brands
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Brend (ID={request.Id}) nije pronađen.");

            // Opcionalno (preporučeno) – zabrani brisanje ako postoje Products:
            var hasProducts = await ctx.Products.AnyAsync(p => p.BrandEntityId == request.Id, ct);
            if (hasProducts)
                throw new MarketConflictException("Brand cannot be deleted because it has products.");

            ctx.Brands.Remove(entity);
            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
