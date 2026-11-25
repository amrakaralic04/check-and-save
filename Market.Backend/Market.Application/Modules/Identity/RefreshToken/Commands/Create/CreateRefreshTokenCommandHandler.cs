namespace Market.Application.Modules.Identity.RefreshToken.Commands.Create;

public sealed class CreateRefreshTokenCommandHandler(IAppDbContext ctx)
    : IRequestHandler<CreateRefreshTokenCommand, int>
{
    public async Task<int> Handle(CreateRefreshTokenCommand request, CancellationToken ct)
    {
        var userExists = await ctx.Users.AnyAsync(x => x.Id == request.UserId, ct);
        if (!userExists)
            throw new MarketNotFoundException($"User (ID={request.UserId}) not found.");

        var tokenHash = request.TokenHash.Trim();

        var exists = await ctx.RefreshTokens
            .AnyAsync(x => x.TokenHash == tokenHash, ct);

        if (exists)
            throw new MarketConflictException("Refresh token already exists.");

        var entity = new RefreshTokenEntity
        {
            TokenHash = tokenHash,
            ExpiresAtUtc = request.ExpiresAtUtc,
            IsRevoked = false,
            UserId = request.UserId,
            Fingerprint = request.Fingerprint?.Trim()
        };

        ctx.RefreshTokens.Add(entity);
        await ctx.SaveChangesAsync(ct);

        return entity.Id;
    }
}