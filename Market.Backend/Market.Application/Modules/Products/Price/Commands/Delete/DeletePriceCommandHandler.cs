namespace Market.Application.Modules.Products.Price.Commands.Delete
{
    public sealed class DeletePriceCommandHandler(IAppDbContext ctx) : IRequestHandler<DeletePriceCommand, Unit>
    {
        public async Task<Unit> Handle(DeletePriceCommand request, CancellationToken ct)
        {
            var entity = await ctx.Prices
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Price (ID={request.Id}) nije pronađen.");

            ctx.Prices.Remove(entity);
            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
