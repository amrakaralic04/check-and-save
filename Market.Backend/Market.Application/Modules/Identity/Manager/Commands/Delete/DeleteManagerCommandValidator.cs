namespace Market.Application.Modules.Identity.Manager.Commands.Delete
{
    public sealed class DeleteManagerCommandValidator : AbstractValidator<DeleteManagerCommand>
    {
        public DeleteManagerCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
