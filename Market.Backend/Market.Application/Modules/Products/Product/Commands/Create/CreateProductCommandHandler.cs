using Market.Domain.Entities.ProductEntities;

namespace Market.Application.Modules.Products.Product.Commands.Create
{
    public sealed class CreateProductCommandHandler(IAppDbContext ctx) : IRequestHandler<CreateProductCommand, int>
    {
        public async Task<int> Handle(CreateProductCommand request, CancellationToken ct)
        {
            var exists = await ctx.Products.AnyAsync(x =>
                x.BranchEntityId == request.BranchEntityId
                && x.Name.ToLower() == request.Name.ToLower(), ct);

            if (exists)
                throw new MarketConflictException("Product name already exists in this branch.");

            var entity = new ProductEntity
            {
                StoreEntityId = request.StoreEntityId,
                BranchEntityId = request.BranchEntityId,
                CategoryEntityId = request.CategoryEntityId,
                BrandEntityId = request.BrandEntityId,
                Name = request.Name.Trim(),
                Description = request.Description.Trim(),
                ImageURL = request.ImageURL.Trim(),
                DateAdded = request.DateAdded
            };

            await ctx.Products.AddAsync(entity, ct);
            await ctx.SaveChangesAsync(ct);

            return entity.Id;
        }
    }
}
