namespace Market.Application.Modules.Analiytics.SaleStatistic.Commands.Update
{
    public sealed class UpdateSalesStatisticCommandHandler(IAppDbContext ctx)
    : IRequestHandler<UpdateSalesStatisticCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateSalesStatisticCommand request, CancellationToken ct)
        {
            var entity = await ctx.SaleStatistics
                .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, ct);

            if (entity is null)
                throw new MarketNotFoundException($"SalesStatistic (ID={request.Id}) not found.");

            bool exists = await ctx.SaleStatistics
                .AnyAsync(x => !x.IsDeleted
                               && x.Id != request.Id
                               && x.ManagerEntityId == entity.ManagerEntityId
                               && x.ProductEntityId == entity.ProductEntityId
                               && x.Date.Date == request.Date.Date, ct);

            if (exists)
                throw new MarketConflictException("Statistic for this manager/product/date already exists.");

            entity.ViewsCount = request.ViewsCount;
            entity.SalesCount = request.SalesCount;
            entity.Date = request.Date;
            entity.ModifiedAt = DateTime.UtcNow;

            await ctx.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}