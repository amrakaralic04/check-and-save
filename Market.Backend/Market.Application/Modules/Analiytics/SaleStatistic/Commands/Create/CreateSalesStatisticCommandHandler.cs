using Market.Domain.Entities.Analytics;

namespace Market.Application.Modules.Analiytics.SaleStatistic.Commands.Create
{
    public sealed class CreateSalesStatisticCommandHandler(IAppDbContext ctx)
    : IRequestHandler<CreateSalesStatisticCommand, int>
    {
        public async Task<int> Handle(CreateSalesStatisticCommand request, CancellationToken ct)
        {
            bool exists = await ctx.SaleStatistics
                .AnyAsync(x => !x.IsDeleted
                               && x.ManagerEntityId == request.ManagerEntityId
                               && x.ProductEntityId == request.ProductEntityId
                               && x.Date.Date == request.Date.Date, ct);

            if (exists)
                throw new MarketConflictException("Statistic for this manager/product/date already exists.");

            var entity = new SalesStatisticEntity
            {
                ManagerEntityId = request.ManagerEntityId,
                ProductEntityId = request.ProductEntityId,
                ViewsCount = request.ViewsCount,
                SalesCount = request.SalesCount,
                Date = request.Date,

                CreatedAt = DateTime.UtcNow,
                ModifiedAt = null,
                IsDeleted = false
            };

            ctx.SaleStatistics.Add(entity);
            await ctx.SaveChangesAsync(ct);

            return entity.Id;
        }
    }
}