namespace Market.Application.Modules.Identity.RefreshToken.Commands.Revoke;

public sealed class RevokeRefreshTokenCommandHandler(IAppDbContext ctx)
    : IRequestHandler<RevokeRefreshTokenCommand, Unit>
{
    public async Task<Unit> Handle(RevokeRefreshTokenCommand request, CancellationToken ct)
    {
        var entity = await ctx.RefreshTokens
            .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

        if (entity is null)
            throw new MarketNotFoundException($"Refresh token (ID={request.Id}) not found.");

        if (!entity.IsRevoked)
        {
            entity.IsRevoked = true;
            entity.RevokedAtUtc = DateTime.UtcNow;
            await ctx.SaveChangesAsync(ct);
        }

        return Unit.Value;
    }
}