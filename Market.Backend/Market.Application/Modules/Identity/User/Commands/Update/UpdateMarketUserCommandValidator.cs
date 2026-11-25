namespace Market.Application.Modules.Identity.User.Commands.Update
{
    public sealed class UpdateMarketUserCommandValidator : AbstractValidator<UpdateMarketUserCommand>
    {
        public UpdateMarketUserCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

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
        }
    }
}