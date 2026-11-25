namespace Market.Application.Modules.Products.ItemComparison.Commands.Delete
{
    public sealed class DeleteItemComparisonCommandHandler(IAppDbContext ctx) : IRequestHandler<DeleteItemComparisonCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteItemComparisonCommand request, CancellationToken ct)
        {
            var entity = await ctx.ItemComparisons
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"ItemComparison (ID={request.Id}) nije pronađen.");

            ctx.ItemComparisons.Remove(entity);
            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
