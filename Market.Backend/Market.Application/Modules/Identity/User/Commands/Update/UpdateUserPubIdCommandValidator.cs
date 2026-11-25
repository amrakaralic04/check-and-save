namespace Market.Application.Modules.Identity.User.Commands.Update;

public sealed class UpdateUserPubIdCommandValidator : AbstractValidator<UpdateUserPubIdCommand>
{
    public UpdateUserPubIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Firstname)
            .NotEmpty()
            .MaximumLength(MarketUserEntity.Constraints.FirstnameMaxLength);

        RuleFor(x => x.Lastname)
            .NotEmpty()
            .MaximumLength(MarketUserEntity.Constraints.LastnameMaxLength);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(MarketUserEntity.Constraints.EmailMaxLength);

        RuleFor(x => x.AvatarLevel)
            .Must(avatarLevel => avatarLevel is 1 or 2)
            .WithMessage("Avatar must be male or female.");
    }
}