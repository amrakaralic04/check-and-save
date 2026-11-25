using Market.Domain.Entities.NotificationEntities;

namespace Market.Application.Modules.Notifications.NotificationType.Commands.Update
{
    public sealed class UpdateNotifTypeCmdValidator : AbstractValidator<UpdateNotifTypeCmd>
    {
        public UpdateNotifTypeCmdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(NotificationTypeEntity.Constraints.NameMaxLength);

            RuleFor(x => x.Description)
                .MaximumLength(NotificationTypeEntity.Constraints.DescriptionMaxLength);
        }
    }
}
