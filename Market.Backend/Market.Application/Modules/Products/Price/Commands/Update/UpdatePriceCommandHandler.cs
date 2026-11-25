namespace Market.Application.Modules.Products.Price.Commands.Update
{
    public sealed class UpdatePriceCommandHandler(IAppDbContext ctx) : IRequestHandler<UpdatePriceCommand, Unit>
    {
        public async Task<Unit> Handle(UpdatePriceCommand request, CancellationToken ct)
        {
            var entity = await ctx.Prices
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Price (ID={request.Id}) nije pronađen.");

            var exists = await ctx.Prices
                .AnyAsync(x => x.Id != request.Id
                            && x.ProductEntityId == entity.ProductEntityId
                            && x.DateUpdated == request.DateUpdated, ct);

            if (exists)
                throw new MarketConflictException("Price for this date already exists.");

            entity.Amount = request.Amount;
            entity.DateUpdated = request.DateUpdated;

            await ctx.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
