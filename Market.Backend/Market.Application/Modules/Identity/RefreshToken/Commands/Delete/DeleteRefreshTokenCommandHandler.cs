namespace Market.Application.Modules.Identity.RefreshToken.Commands.Delete;

public sealed class DeleteRefreshTokenCommandHandler(IAppDbContext ctx)
    : IRequestHandler<DeleteRefreshTokenCommand, Unit>
{
    public async Task<Unit> Handle(DeleteRefreshTokenCommand request, CancellationToken ct)
    {
        var entity = await ctx.RefreshTokens
            .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

        if (entity is null)
            throw new MarketNotFoundException("Refresh token not found.");

        ctx.RefreshTokens.Remove(entity);
        await ctx.SaveChangesAsync(ct);

        return Unit.Value;
    }
}