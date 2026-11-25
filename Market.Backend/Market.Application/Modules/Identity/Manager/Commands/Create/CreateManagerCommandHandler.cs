namespace Market.Application.Modules.Identity.Manager.Commands.Create
{
    public sealed class CreateManagerCommandHandler(IAppDbContext ctx)  : IRequestHandler<CreateManagerCommand, int>
    {
        public async Task<int> Handle(CreateManagerCommand request, CancellationToken ct)
        {

            var userExists = await ctx.Managers
                .AnyAsync(x => x.MarketUserEntityId == request.MarketUserEntityId, ct);

            if (userExists)
                throw new MarketConflictException("This user is already a manager.");

            var storeExists = await ctx.Managers
                .AnyAsync(x => x.StoreEntityId == request.StoreEntityId, ct);

            if (storeExists)
                throw new MarketConflictException("This store already has a manager.");

            var entity = new ManagerEntity
            {
                MarketUserEntityId = request.MarketUserEntityId,
                StoreEntityId = request.StoreEntityId,
                StartDate = request.StartDate
            };

            await ctx.Managers.AddAsync(entity, ct);
            await ctx.SaveChangesAsync(ct);

            return entity.Id;
        }
    }
}
