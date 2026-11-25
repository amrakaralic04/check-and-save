namespace Market.Application.Modules.Identity.Manager.Commands.Delete
{
    public sealed class DeleteManagerCommandHandler(IAppDbContext ctx) : IRequestHandler<DeleteManagerCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteManagerCommand request, CancellationToken ct)
        {
            var entity = await ctx.Managers
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Manager (ID={request.Id}) nije pronađen.");

            ctx.Managers.Remove(entity);
            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
