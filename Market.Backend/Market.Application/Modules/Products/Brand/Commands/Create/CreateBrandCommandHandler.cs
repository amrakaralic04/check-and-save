using Market.Domain.Entities.ProductEntities;

namespace Market.Application.Modules.Products.Brand.Commands.Create
{
    public sealed class CreateBrandCommandHandler(IAppDbContext ctx) : IRequestHandler<CreateBrandCommand, int>
    {
        public async Task<int> Handle(CreateBrandCommand request, CancellationToken ct)
        {
            var exists = await ctx.Brands
                .AnyAsync(x => x.Name.ToLower() == request.Name.ToLower(), ct);

            if (exists)
                throw new MarketConflictException("Name already exists.");

            var entity = new BrandEntity
            {
                Name = request.Name.Trim(),
                Description = request.Description?.Trim()
            };

            await ctx.Brands.AddAsync(entity, ct);
            await ctx.SaveChangesAsync(ct);

            return entity.Id;
        }
    }
}
