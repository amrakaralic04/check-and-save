namespace Market.Application.Modules.Identity.RefreshToken.Commands.Delete;

public sealed class DeleteRefreshTokenCommand : IRequest<Unit>
{
    public int Id { get; set; }
}