namespace Market.Application.Modules.Analiytics.SaleStatistic.Commands.Delete
{
    public sealed class DeleteSalesStatisticCommandHandler(IAppDbContext ctx)
    : IRequestHandler<DeleteSalesStatisticCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteSalesStatisticCommand request, CancellationToken ct)
        {
            var entity = await ctx.SaleStatistics
                .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, ct);

            if (entity is null)
                throw new MarketNotFoundException($"SalesStatistic (ID={request.Id}) not found.");

            entity.IsDeleted = true;
            entity.ModifiedAt = DateTime.UtcNow;

            await ctx.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}