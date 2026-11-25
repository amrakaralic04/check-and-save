namespace Market.Application.Modules.Products.Comparison.Commands.Delete
{
    public sealed class DeleteComparisonCommandHandler(IAppDbContext ctx) : IRequestHandler<DeleteComparisonCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteComparisonCommand request, CancellationToken ct)
        {
            var entity = await ctx.Comparisons
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException(
                    $"Comparison (ID={request.Id}) nije pronađen.");

            ctx.Comparisons.Remove(entity);
            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
