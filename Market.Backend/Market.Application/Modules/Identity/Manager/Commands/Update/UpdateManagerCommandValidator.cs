namespace Market.Application.Modules.Identity.Manager.Commands.Update
{
    public sealed class UpdateManagerCommandValidator : AbstractValidator<UpdateManagerCommand>
    {
        public UpdateManagerCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.StoreEntityId).GreaterThan(0);
            RuleFor(x => x.StartDate).NotEmpty();
        }
    }
}
