namespace Market.Application.Modules.Identity.Manager.Commands.Create
{
    public sealed class CreateManagerCommandValidator : AbstractValidator<CreateManagerCommand>
    {
        public CreateManagerCommandValidator()
        {
            RuleFor(x => x.MarketUserEntityId).GreaterThan(0);
            RuleFor(x => x.StoreEntityId).GreaterThan(0);
            RuleFor(x => x.StartDate).NotEmpty();
        }
        
    }
}
