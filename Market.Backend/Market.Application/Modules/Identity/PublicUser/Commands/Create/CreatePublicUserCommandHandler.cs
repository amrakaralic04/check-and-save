namespace Market.Application.Modules.Identity.PublicUser.Commands.Create;

public class CreatePublicUserCommandHandler(IAppDbContext ctx)
    : IRequestHandler<CreatePublicUserCommand, int>
{
    public async Task<int> Handle(CreatePublicUserCommand request, CancellationToken ct)
    {
        var exists = await ctx.PublicUsers
            .AnyAsync(x => x.MarketUserEntityId == request.MarketUserEntityId, ct);

        if (exists)
            throw new MarketConflictException("Public user for this market user already exists.");

        var entity = new PublicUserEntity
        {
            MarketUserEntityId = request.MarketUserEntityId,
            Points = request.Points,
            AvatarLevel = request.AvatarLevel
        };

        ctx.PublicUsers.Add(entity);
        await ctx.SaveChangesAsync(ct);

        return entity.Id;
    }
}