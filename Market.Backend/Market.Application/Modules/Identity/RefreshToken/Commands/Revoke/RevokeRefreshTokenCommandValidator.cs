namespace Market.Application.Modules.Identity.RefreshToken.Commands.Revoke;

public sealed class RevokeRefreshTokenCommandValidator
    : AbstractValidator<RevokeRefreshTokenCommand>
{
    public RevokeRefreshTokenCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}