namespace Market.Application.Modules.Identity.PublicUser.Commands.Update;

public sealed class UpdatePublicUserCommandValidator
    : AbstractValidator<UpdatePublicUserCommand>
{
    public UpdatePublicUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Points)
            .GreaterThanOrEqualTo(PublicUserEntity.Constraints.MinPoints)
            .WithMessage($"Points must be at least {PublicUserEntity.Constraints.MinPoints}.");

        RuleFor(x => x.AvatarLevel)
            .GreaterThanOrEqualTo(PublicUserEntity.Constraints.MinAvatarLevel)
            .LessThanOrEqualTo(PublicUserEntity.Constraints.MaxAvatarLevel)
            .WithMessage($"Avatar level must be between {PublicUserEntity.Constraints.MinAvatarLevel} and {PublicUserEntity.Constraints.MaxAvatarLevel}.");
    }
}