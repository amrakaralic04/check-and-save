namespace Market.Application.Modules.Identity.RefreshToken.Commands.Create;

public sealed class CreateRefreshTokenCommandValidator
    : AbstractValidator<CreateRefreshTokenCommand>
{
    public CreateRefreshTokenCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0);

        RuleFor(x => x.TokenHash)
            .NotEmpty().WithMessage("Token hash is required.")
            .MaximumLength(RefreshTokenEntity.Constraints.TokenHashMaxLength);

        RuleFor(x => x.ExpiresAtUtc)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Expiration must be in the future.");

        RuleFor(x => x.Fingerprint)
            .MaximumLength(RefreshTokenEntity.Constraints.FingerprintMaxLength)
            .When(x => x.Fingerprint != null);
    }
}