namespace Market.Application.Modules.Auth.Commands.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(MarketUserEntity.Constraints.FirstnameMaxLength);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(MarketUserEntity.Constraints.LastnameMaxLength);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(MarketUserEntity.Constraints.EmailMaxLength);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.Gender)
            .NotEmpty()
            .Must(gender => gender.Equals("male", StringComparison.OrdinalIgnoreCase) || gender.Equals("female", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Gender must be male or female.");
    }
}