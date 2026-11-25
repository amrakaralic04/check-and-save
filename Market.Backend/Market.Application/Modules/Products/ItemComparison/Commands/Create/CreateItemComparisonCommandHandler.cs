using Market.Domain.Entities.ProductEntities;

namespace Market.Application.Modules.Products.ItemComparison.Commands.Create
{
    public sealed class CreateItemComparisonCommandHandler(IAppDbContext ctx) : IRequestHandler<CreateItemComparisonCommand, int>
    {
        public async Task<int> Handle(CreateItemComparisonCommand request, CancellationToken ct)
        {
            var exists = await ctx.ItemComparisons
                .AnyAsync(x => x.ComparisonEntityId == request.ComparisonEntityId
                            && x.ProductId == request.ProductId, ct);

            if (exists)
                throw new MarketConflictException("Item comparison already exists.");

            var entity = new ItemComparisonEntity
            {
                ComparisonEntityId = request.ComparisonEntityId,
                ProductId = request.ProductId
            };

            await ctx.ItemComparisons.AddAsync(entity, ct);
            await ctx.SaveChangesAsync(ct);

            return entity.Id;
        }
    }
}
