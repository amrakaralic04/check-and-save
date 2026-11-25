namespace Market.Application.Modules.Identity.Activity.Commands.Create
{
    public sealed class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
    {
        public CreateActivityCommandValidator()
        {
            RuleFor(x => x.MarketUserEntityId).GreaterThan(0);

            RuleFor(x => x.ActivityType)
                .NotEmpty()
                .MaximumLength(ActivityEntity.Constraints.ActivityTypeMaxLength);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(ActivityEntity.Constraints.DescriptionMaxLength);

            RuleFor(x => x.Date).NotEmpty();
        }
    }
}
