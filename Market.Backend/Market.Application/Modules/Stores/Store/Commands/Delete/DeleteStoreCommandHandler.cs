namespace Market.Application.Modules.Store.Stores.Commands.Delete;

public class DeleteStoreCommandHandler(IAppDbContext ctx)
    : IRequestHandler<DeleteStoreCommand, Unit>
{
    public async Task<Unit> Handle(DeleteStoreCommand request, CancellationToken ct)
    {
        var entity = await ctx.Stores
            .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

        if (entity is null)
            throw new MarketNotFoundException("Store not found.");

        ctx.Stores.Remove(entity);
        await ctx.SaveChangesAsync(ct);

        return Unit.Value;
    }
}