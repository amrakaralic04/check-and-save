namespace Market.Application.Modules.Identity.PublicUser.Commands.Delete;

public class DeletePublicUserCommandHandler(IAppDbContext ctx)
    : IRequestHandler<DeletePublicUserCommand, Unit>
{
    public async Task<Unit> Handle(DeletePublicUserCommand request, CancellationToken ct)
    {
        var entity = await ctx.PublicUsers
            .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

        if (entity is null)
            throw new MarketNotFoundException("Public user not found.");

        ctx.PublicUsers.Remove(entity);
        await ctx.SaveChangesAsync(ct);

        return Unit.Value;
    }
}