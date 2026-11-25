namespace Market.Application.Modules.Identity.User.Commands.Create
{
    public sealed class CreateMarketUserCommandValidator : AbstractValidator<CreateMarketUserCommand>
    {
        public CreateMarketUserCommandValidator()
        {
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

            RuleFor(x => x.PasswordHash)
                .NotEmpty();
        }
    }
}
