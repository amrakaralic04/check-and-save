namespace Market.Application.Modules.Identity.RefreshToken.Queries.List;

public sealed class ListRefreshTokensQuery : BasePagedQuery<ListRefreshTokensQueryDto>
{
    public int? UserId { get; init; }
    public bool? OnlyActive { get; init; } // aktivni = !IsRevoked && ExpiresAtUtc > now
}