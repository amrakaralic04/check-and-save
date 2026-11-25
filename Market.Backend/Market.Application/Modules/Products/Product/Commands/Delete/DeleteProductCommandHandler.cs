namespace Market.Application.Modules.Products.Product.Commands.Delete
{
    public sealed class DeleteProductCommandHandler(IAppDbContext ctx) : IRequestHandler<DeleteProductCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken ct)
        {
            var entity = await ctx.Products
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Product (ID={request.Id}) nije pronađen.");

            ctx.Products.Remove(entity);
            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
