namespace Market.Application.Modules.Products.Comparison.Commands.Update
{
    public sealed class UpdateComparisonCommandHandler(IAppDbContext ctx) : IRequestHandler<UpdateComparisonCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateComparisonCommand request, CancellationToken ct)
        {
            var entity = await ctx.Comparisons
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException(
                    $"Comparison (ID={request.Id}) nije pronađen.");

            entity.Date = request.Date;

            await ctx.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
