namespace Market.Application.Modules.Identity.User.Commands.Status.Disable
{
    public sealed class DisableMarketUserCommandValidator : AbstractValidator<DisableMarketUserCommand>
    {
        public DisableMarketUserCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
