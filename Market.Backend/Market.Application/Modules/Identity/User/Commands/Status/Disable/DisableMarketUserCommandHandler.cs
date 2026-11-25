namespace Market.Application.Modules.Identity.User.Commands.Status.Disable
{
    public sealed class DisableMarketUserCommandHandler(IAppDbContext ctx) : IRequestHandler<DisableMarketUserCommand, Unit>
    {
        public async Task<Unit> Handle(DisableMarketUserCommand request, CancellationToken ct)
        {
            var user = await ctx.Users
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (user is null)
                throw new MarketNotFoundException($"Korisnik (ID={request.Id}) nije pronađen.");

            if (!user.IsEnabled) return Unit.Value; 

            if (user.IsAdmin)
            {
                throw new MarketBusinessRuleException(
                    "user.disable.blocked.admin",
                    $"Admin korisnik (ID={user.Id}) se ne može deaktivirati.");
            }

            user.IsEnabled = false;
            user.TokenVersion += 1;

            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
