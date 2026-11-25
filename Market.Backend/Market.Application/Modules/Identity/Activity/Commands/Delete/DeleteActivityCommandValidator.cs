namespace Market.Application.Modules.Identity.Activity.Commands.Delete
{
    public sealed class DeleteActivityCommandValidator : AbstractValidator<DeleteActivityCommand>
    {
        public DeleteActivityCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
