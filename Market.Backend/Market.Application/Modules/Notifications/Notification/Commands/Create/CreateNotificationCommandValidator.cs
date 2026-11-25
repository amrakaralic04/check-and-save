using Market.Domain.Entities.NotificationEntities;

namespace Market.Application.Modules.Notifications.Notification.Commands.Create
{
    public sealed class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationCommandValidator()
        {
            RuleFor(x => x.MarketUserEntityId)
                .GreaterThan(0).WithMessage("Market user is required.");

            RuleFor(x => x.NotificationTypeEntityId)
                .GreaterThan(0).WithMessage("Notification type is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(NotificationEntity.Constraints.TitleMaxLength)
                .WithMessage($"Title can be at most {NotificationEntity.Constraints.TitleMaxLength} characters long.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.")
                .MaximumLength(NotificationEntity.Constraints.MessageMaxLength)
                .WithMessage($"Message can be at most {NotificationEntity.Constraints.MessageMaxLength} characters long.");
        }
    }
}
