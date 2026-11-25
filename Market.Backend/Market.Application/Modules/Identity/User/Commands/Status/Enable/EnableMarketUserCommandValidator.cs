namespace Market.Application.Modules.Identity.User.Commands.Status.Enable
{
    public sealed class EnableMarketUserCommandValidator : AbstractValidator<EnableMarketUserCommand>
    {
        public EnableMarketUserCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
