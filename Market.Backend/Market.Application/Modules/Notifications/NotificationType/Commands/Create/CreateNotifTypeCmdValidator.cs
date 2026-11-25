using Market.Domain.Entities.NotificationEntities;

namespace Market.Application.Modules.Notifications.NotificationType.Commands.Create
{
    public sealed class CreateNotifTypeCmdValidator : AbstractValidator<CreateNotifTypeCmd>
    {
        public CreateNotifTypeCmdValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(NotificationTypeEntity.Constraints.NameMaxLength);

            RuleFor(x => x.Description)
                .MaximumLength(NotificationTypeEntity.Constraints.DescriptionMaxLength);
        }
    }
}
