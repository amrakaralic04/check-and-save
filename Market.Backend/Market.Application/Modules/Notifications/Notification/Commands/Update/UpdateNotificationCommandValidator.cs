using Market.Domain.Entities.NotificationEntities;

namespace Market.Application.Modules.Notifications.Notification.Commands.Update
{
    public sealed class UpdateNotificationCommandValidator : AbstractValidator<UpdateNotificationCommand>
    {
        public UpdateNotificationCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

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
