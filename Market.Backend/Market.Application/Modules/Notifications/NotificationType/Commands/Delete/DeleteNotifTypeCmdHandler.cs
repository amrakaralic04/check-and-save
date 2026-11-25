namespace Market.Application.Modules.Notifications.NotificationType.Commands.Delete
{
    public sealed class DeleteNotifTypeCmdHandler(IAppDbContext context, IAppCurrentUser appCurrentUser): IRequestHandler<DeleteNotifTypeCmd, Unit>
    {
        public async Task<Unit> Handle(DeleteNotifTypeCmd request, CancellationToken cancellationToken)
        {
            if (appCurrentUser.UserId is null)
                throw new MarketBusinessRuleException("123", "Korisnik nije autentifikovan.");

            var entity = await context.NotificationTypes
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
                throw new MarketNotFoundException("Tip notifikacije nije pronađen.");

            context.NotificationTypes.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
