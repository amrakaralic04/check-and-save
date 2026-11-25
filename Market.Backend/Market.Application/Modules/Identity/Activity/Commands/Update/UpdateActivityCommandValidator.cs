namespace Market.Application.Modules.Identity.Activity.Commands.Update
{
    public sealed class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
    {
        public UpdateActivityCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

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
