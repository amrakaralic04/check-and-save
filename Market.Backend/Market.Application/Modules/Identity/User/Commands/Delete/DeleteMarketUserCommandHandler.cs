namespace Market.Application.Modules.Identity.User.Commands.Delete
{
    public sealed class DeleteMarketUserCommandHandler(IAppDbContext ctx) : IRequestHandler<DeleteMarketUserCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteMarketUserCommand request, CancellationToken ct)
        {
            var entity = await ctx.Users.FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (entity is null)
                throw new MarketNotFoundException($"User (ID={request.Id}) nije pronađen.");

            ctx.Users.Remove(entity);
            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
