using Market.Domain.Entities.NotificationEntities;

namespace Market.Application.Modules.Notifications.NotificationType.Commands.Create
{
    public sealed class CreateNotifTypeCmdHandler(IAppDbContext context) : IRequestHandler<CreateNotifTypeCmd, int>
    {
        public async Task<int> Handle(CreateNotifTypeCmd request, CancellationToken cancellationToken)
        {
            var normalized = request.Name?.Trim();

            if (string.IsNullOrWhiteSpace(normalized))
                throw new ValidationException("Name is required.");

            bool exists = await context.NotificationTypes
                .AnyAsync(x => x.Name == normalized, cancellationToken);

            if (exists)
                throw new MarketConflictException("Name already exists.");

            var entity = new NotificationTypeEntity
            {
                Name = normalized,
                Description = request.Description?.Trim()
            };

            context.NotificationTypes.Add(entity);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
