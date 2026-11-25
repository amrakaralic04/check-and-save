namespace Market.Application.Modules.Notifications.Notification.Commands.Delete
{
    public sealed class DeleteNotificationCommandHandler(IAppDbContext context, IAppCurrentUser appCurrentUser): IRequestHandler<DeleteNotificationCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            if (appCurrentUser.UserId is null)
                throw new MarketBusinessRuleException("123", "Korisnik nije autentifikovan.");

            var notification = await context.Notifications
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (notification is null)
                throw new MarketNotFoundException("Notifikacija nije pronađena.");

            context.Notifications.Remove(notification);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
