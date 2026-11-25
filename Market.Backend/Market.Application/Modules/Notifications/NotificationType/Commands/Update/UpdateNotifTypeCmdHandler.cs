namespace Market.Application.Modules.Notifications.NotificationType.Commands.Update
{
    public sealed class UpdateNotifTypeCmdHandler(IAppDbContext ctx) : IRequestHandler<UpdateNotifTypeCmd, Unit>
    {
        public async Task<Unit> Handle(UpdateNotifTypeCmd request, CancellationToken ct)
        {
            var entity = await ctx.NotificationTypes
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Notification type (ID={request.Id}) not found.");

            var exists = await ctx.NotificationTypes
                .AnyAsync(x => x.Id != request.Id && x.Name.ToLower() == request.Name.ToLower(), ct);

            if (exists)
                throw new MarketConflictException("Name already exists.");

            entity.Name = request.Name.Trim();
            entity.Description = request.Description?.Trim();

            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
