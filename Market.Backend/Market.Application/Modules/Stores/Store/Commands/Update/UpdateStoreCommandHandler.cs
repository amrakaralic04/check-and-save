namespace Market.Application.Modules.Stores.Store.Commands.Update;

public sealed class UpdateStoreCommandHandler(IAppDbContext ctx)
    : IRequestHandler<UpdateStoreCommand, Unit>
{
    public async Task<Unit> Handle(UpdateStoreCommand request, CancellationToken ct)
    {
        var entity = await ctx.Stores
            .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

        if (entity is null)
            throw new MarketNotFoundException($"Store (ID={request.Id}) not found.");

        var cityExists = await ctx.Cities
            .AnyAsync(x => x.Id == request.CityEntityId, ct);

        if (!cityExists)
            throw new MarketNotFoundException($"City (ID={request.CityEntityId}) not found.");

        var name = request.Name.Trim();
        var contact = request.Contact.Trim();
        var email = request.Email.Trim();

        var exists = await ctx.Stores.AnyAsync(
            x => x.Id != request.Id &&
                 x.CityEntityId == request.CityEntityId &&
                 x.Name.ToLower() == name.ToLower(),
            ct);

        if (exists)
            throw new MarketConflictException("Store with same name in this city already exists.");

        entity.Name = name;
        entity.Contact = contact;
        entity.Email = email;
        entity.CityEntityId = request.CityEntityId;
        entity.IsActive = request.IsActive;

        await ctx.SaveChangesAsync(ct);

        return Unit.Value;
    }
}