namespace Market.Application.Modules.Notifications.Notification.Queries.GetById
{
    public sealed class GetNotificationByIdQueryValidator : AbstractValidator<GetNotificationByIdQuery>
    {
        public GetNotificationByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id must be a positive value.");
        }
    }
}
