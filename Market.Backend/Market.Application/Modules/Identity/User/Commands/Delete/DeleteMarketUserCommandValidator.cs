namespace Market.Application.Modules.Identity.User.Commands.Delete
{
    public sealed class DeleteMarketUserCommandValidator : AbstractValidator<DeleteMarketUserCommand>
    {
        public DeleteMarketUserCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
