namespace Market.Application.Modules.Notifications.Notification.Commands.Update
{
    public sealed class UpdateNotificationCommandHandler(IAppDbContext ctx) : IRequestHandler<UpdateNotificationCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateNotificationCommand request, CancellationToken ct)
        {
            var entity = await ctx.Notifications
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Notifikacija (ID={request.Id}) nije pronađena.");

 
            var exists = await ctx.Notifications
                .AnyAsync(x =>
                    x.Id != request.Id &&
                    x.MarketUserEntityId == entity.MarketUserEntityId &&
                    x.Title.ToLower() == request.Title.ToLower(),
                    ct);

            if (exists)
            {
                throw new MarketConflictException("Notification with the same title already exists for this user.");
            }

            entity.Title = request.Title.Trim();
            entity.Message = request.Message.Trim();
            entity.IsRead = request.IsRead;

            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
