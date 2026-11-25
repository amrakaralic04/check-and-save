namespace Market.Application.Modules.Identity.PublicUser.Commands.Update;

public sealed class UpdatePublicUserCommandHandler(IAppDbContext ctx)
    : IRequestHandler<UpdatePublicUserCommand, Unit>
{
    public async Task<Unit> Handle(UpdatePublicUserCommand request, CancellationToken ct)
    {
        var entity = await ctx.PublicUsers
            .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

        if (entity is null)
            throw new MarketNotFoundException($"Public user (ID={request.Id}) not found.");

        entity.Points = request.Points;
        entity.AvatarLevel = request.AvatarLevel;

        await ctx.SaveChangesAsync(ct);

        return Unit.Value;
    }
}