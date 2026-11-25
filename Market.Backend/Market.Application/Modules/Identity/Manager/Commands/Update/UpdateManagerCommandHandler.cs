namespace Market.Application.Modules.Identity.Manager.Commands.Update
{
    public sealed class UpdateManagerCommandHandler(IAppDbContext ctx) : IRequestHandler<UpdateManagerCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateManagerCommand request, CancellationToken ct)
        {
            var entity = await ctx.Managers
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Manager (ID={request.Id}) nije pronađen.");

            var storeTaken = await ctx.Managers.AnyAsync(x =>
                x.Id != request.Id && x.StoreEntityId == request.StoreEntityId, ct);

            if (storeTaken)
                throw new MarketConflictException("This store already has a manager.");

            entity.StoreEntityId = request.StoreEntityId;
            entity.StartDate = request.StartDate;

            await ctx.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
