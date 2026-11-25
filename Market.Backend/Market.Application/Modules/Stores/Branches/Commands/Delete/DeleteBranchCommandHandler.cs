namespace Market.Application.Modules.Stores.Branches.Commands.Delete;

public class DeleteBranchCommandHandler(IAppDbContext ctx)
    : IRequestHandler<DeleteBranchCommand, Unit>
{
    public async Task<Unit> Handle(DeleteBranchCommand request, CancellationToken ct)
    {
        var entity = await ctx.Branches
            .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

        if (entity is null)
            throw new MarketNotFoundException("Branch not found.");

        ctx.Branches.Remove(entity);
        await ctx.SaveChangesAsync(ct);

        return Unit.Value;
    }
}