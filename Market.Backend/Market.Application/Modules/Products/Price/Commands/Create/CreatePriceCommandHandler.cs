using Market.Domain.Entities.ProductEntities;

namespace Market.Application.Modules.Products.Price.Commands.Create
{
    public sealed class CreatePriceCommandHandler(IAppDbContext ctx) : IRequestHandler<CreatePriceCommand, int>
    {
        public async Task<int> Handle(CreatePriceCommand request, CancellationToken ct)
        { 
            var exists = await ctx.Prices
                .AnyAsync(x => x.ProductEntityId == request.ProductEntityId
                            && x.DateUpdated == request.DateUpdated, ct);

            if (exists)
                throw new MarketConflictException("Price for this date already exists.");

            var entity = new PriceEntity
            {
                ProductEntityId = request.ProductEntityId,
                Amount = request.Amount,
                DateUpdated = request.DateUpdated
            };

            await ctx.Prices.AddAsync(entity, ct);
            await ctx.SaveChangesAsync(ct);

            return entity.Id;
        }
    }
}
