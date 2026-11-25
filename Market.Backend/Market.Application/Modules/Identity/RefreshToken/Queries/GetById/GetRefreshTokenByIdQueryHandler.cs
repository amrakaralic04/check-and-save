namespace Market.Application.Modules.Identity.RefreshToken.Queries.GetById;

public sealed class GetRefreshTokenByIdQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetRefreshTokenByIdQuery, GetRefreshTokenByIdQueryDto>
{
    public async Task<GetRefreshTokenByIdQueryDto> Handle(
        GetRefreshTokenByIdQuery request, CancellationToken ct)
    {
        var token = await ctx.RefreshTokens
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new GetRefreshTokenByIdQueryDto
            {
                Id = x.Id,
                UserId = x.UserId,
                TokenHash = x.TokenHash,
                ExpiresAtUtc = x.ExpiresAtUtc,
                IsRevoked = x.IsRevoked,
                RevokedAtUtc = x.RevokedAtUtc,
                Fingerprint = x.Fingerprint
            })
            .FirstOrDefaultAsync(ct);

        if (token is null)
            throw new MarketNotFoundException($"Refresh token (ID={request.Id}) not found.");

        return token;
    }
}