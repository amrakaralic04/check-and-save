namespace Market.Application.Modules.Products.ItemComparison.Commands.Update
{
    public sealed class UpdateItemComparisonCommandHandler(IAppDbContext ctx) : IRequestHandler<UpdateItemComparisonCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateItemComparisonCommand request, CancellationToken ct)
        {
            var entity = await ctx.ItemComparisons
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"ItemComparison (ID={request.Id}) nije pronađen.");

            var exists = await ctx.ItemComparisons
                .AnyAsync(x => x.Id != request.Id
                            && x.ComparisonEntityId == entity.ComparisonEntityId
                            && x.ProductId == request.ProductId, ct);

            if (exists)
                throw new MarketConflictException("Item comparison already exists.");

            entity.ProductId = request.ProductId;

            await ctx.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
