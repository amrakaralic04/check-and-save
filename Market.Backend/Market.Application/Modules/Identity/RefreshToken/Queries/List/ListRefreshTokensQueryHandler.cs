namespace Market.Application.Modules.Identity.RefreshToken.Queries.List;

public sealed class ListRefreshTokensQueryHandler(IAppDbContext ctx)
    : IRequestHandler<ListRefreshTokensQuery, PageResult<ListRefreshTokensQueryDto>>
{
    public async Task<PageResult<ListRefreshTokensQueryDto>> Handle(
        ListRefreshTokensQuery request, CancellationToken ct)
    {
        var q = ctx.RefreshTokens.AsNoTracking();

        if (request.UserId is not null)
            q = q.Where(x => x.UserId == request.UserId);

        if (request.OnlyActive is not null && request.OnlyActive.Value)
        {
            var now = DateTime.UtcNow;
            q = q.Where(x => !x.IsRevoked && x.ExpiresAtUtc > now);
        }

        var projectedQuery = q
            .OrderByDescending(x => x.Id)
            .Select(x => new ListRefreshTokensQueryDto
            {
                Id = x.Id,
                UserId = x.UserId,
                TokenHash = x.TokenHash,
                ExpiresAtUtc = x.ExpiresAtUtc,
                IsRevoked = x.IsRevoked,
                RevokedAtUtc = x.RevokedAtUtc,
                Fingerprint = x.Fingerprint
            });

        return await PageResult<ListRefreshTokensQueryDto>.FromQueryableAsync(
            projectedQuery,
            request.Paging,
            ct);
    }
}