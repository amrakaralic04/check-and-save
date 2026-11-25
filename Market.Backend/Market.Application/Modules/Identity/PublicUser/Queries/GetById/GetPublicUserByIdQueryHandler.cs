namespace Market.Application.Modules.Identity.PublicUser.Queries.GetById;

public class GetPublicUserByIdQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetPublicUserByIdQuery, GetPublicUserByIdQueryDto>
{
    public async Task<GetPublicUserByIdQueryDto> Handle(GetPublicUserByIdQuery request, CancellationToken ct)
    {
        var user = await ctx.PublicUsers
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new GetPublicUserByIdQueryDto
            {
                Id = x.Id,
                MarketUserEntityId = x.MarketUserEntityId,
                Points = x.Points,
                AvatarLevel = x.AvatarLevel
            })
            .FirstOrDefaultAsync(ct);

        if (user is null)
            throw new MarketNotFoundException($"Public user with Id {request.Id} not found.");

        return user;
    }
}