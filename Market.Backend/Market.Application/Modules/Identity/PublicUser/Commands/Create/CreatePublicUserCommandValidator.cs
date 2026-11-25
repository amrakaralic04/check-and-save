namespace Market.Application.Modules.Identity.PublicUser.Commands.Create;

public sealed class CreatePublicUserCommandValidator
    : AbstractValidator<CreatePublicUserCommand>
{
    public CreatePublicUserCommandValidator()
    {
        RuleFor(x => x.MarketUserEntityId)
            .GreaterThan(0).WithMessage("MarketUserEntityId must be a positive value.");

        RuleFor(x => x.Points)
            .GreaterThanOrEqualTo(PublicUserEntity.Constraints.MinPoints)
            .WithMessage($"Points must be at least {PublicUserEntity.Constraints.MinPoints}.");

        RuleFor(x => x.AvatarLevel)
            .GreaterThanOrEqualTo(PublicUserEntity.Constraints.MinAvatarLevel)
            .LessThanOrEqualTo(PublicUserEntity.Constraints.MaxAvatarLevel)
            .WithMessage($"Avatar level must be between {PublicUserEntity.Constraints.MinAvatarLevel} and {PublicUserEntity.Constraints.MaxAvatarLevel}.");
    }
}