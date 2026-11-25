using Market.Domain.Entities.NotificationEntities;

namespace Market.Application.Modules.Notifications.Notification.Commands.Create
{
    public sealed class CreateNotificationCommandHandler(IAppDbContext context) : IRequestHandler<CreateNotificationCommand, int>
    {
        public async Task<int> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var normalizedTitle = request.Title?.Trim();
            var normalizedMessage = request.Message?.Trim();

            if (string.IsNullOrWhiteSpace(normalizedTitle))
                throw new ValidationException("Title is required.");

            if (string.IsNullOrWhiteSpace(normalizedMessage))
                throw new ValidationException("Message is required.");

           
            bool userExists = await context.Users
                .AnyAsync(x => x.Id == request.MarketUserEntityId, cancellationToken);

            if (!userExists)
                throw new ValidationException("Market user does not exist.");

            bool typeExists = await context.NotificationTypes
                .AnyAsync(x => x.Id == request.NotificationTypeEntityId, cancellationToken);

            if (!typeExists)
                throw new ValidationException("Notification type does not exist.");

            var notification = new NotificationEntity
            {
                MarketUserEntityId = request.MarketUserEntityId,
                NotificationTypeEntityId = request.NotificationTypeEntityId,
                Title = normalizedTitle,
                Message = normalizedMessage,
                IsRead = request.IsRead
            };

            context.Notifications.Add(notification);
            await context.SaveChangesAsync(cancellationToken);

            return notification.Id;
        }
    }
}
