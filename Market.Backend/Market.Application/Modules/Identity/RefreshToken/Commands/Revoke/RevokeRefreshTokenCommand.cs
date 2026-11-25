namespace Market.Application.Modules.Identity.RefreshToken.Commands.Revoke;

public sealed class RevokeRefreshTokenCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string? Reason { get; set; }
}