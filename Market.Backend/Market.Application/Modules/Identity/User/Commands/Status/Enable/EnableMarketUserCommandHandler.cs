
using Market.Application.Modules.Identity.User.Commands.Status.Enable;

namespace Market.Application.Modules.Identity.Users.Commands.Status.Enable;

public sealed class EnableMarketUserCommandHandler(IAppDbContext ctx) : IRequestHandler<EnableMarketUserCommand, Unit>
{
    public async Task<Unit> Handle(EnableMarketUserCommand request, CancellationToken ct)
    {
        var user = await ctx.Users.FirstOrDefaultAsync(x => x.Id == request.Id, ct);

        if (user is null)
            throw new MarketNotFoundException($"Korisnik (ID={request.Id}) nije pronađen.");

        if (!user.IsEnabled)
        {
            user.IsEnabled = true;
            await ctx.SaveChangesAsync(ct);
        }

        return Unit.Value;
    }
}