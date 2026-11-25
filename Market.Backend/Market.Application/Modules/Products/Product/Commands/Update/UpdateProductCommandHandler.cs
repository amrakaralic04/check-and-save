namespace Market.Application.Modules.Products.Product.Commands.Update
{
    public sealed class UpdateProductCommandHandler(IAppDbContext ctx) : IRequestHandler<UpdateProductCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken ct)
        {
            var entity = await ctx.Products
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Product (ID={request.Id}) nije pronađen.");

            var exists = await ctx.Products.AnyAsync(x =>
                x.Id != request.Id
                && x.BranchEntityId == request.BranchEntityId
                && x.Name.ToLower() == request.Name.ToLower(), ct);

            if (exists)
                throw new MarketConflictException("Product name already exists in this branch.");

            entity.StoreEntityId = request.StoreEntityId;
            entity.BranchEntityId = request.BranchEntityId;
            entity.CategoryEntityId = request.CategoryEntityId;
            entity.BrandEntityId = request.BrandEntityId;
            entity.Name = request.Name.Trim();
            entity.Description = request.Description.Trim();
            entity.ImageURL = request.ImageURL.Trim();

            await ctx.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
