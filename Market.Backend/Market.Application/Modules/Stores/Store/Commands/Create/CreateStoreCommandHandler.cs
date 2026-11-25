using Market.Domain.Entities.StoreEntities;

namespace Market.Application.Modules.Stores.Store.Commands.Create;

public class CreateStoreCommandHandler(IAppDbContext ctx)
    : IRequestHandler<CreateStoreCommand, int>
{
    public async Task<int> Handle(CreateStoreCommand request, CancellationToken ct)
    {
        var cityExists = await ctx.Cities
            .AnyAsync(x => x.Id == request.CityEntityId, ct);

        if (!cityExists)
            throw new MarketNotFoundException($"City (ID={request.CityEntityId}) not found.");

        var name = request.Name.Trim();
        var contact = request.Contact.Trim();
        var email = request.Email.Trim();

        var exists = await ctx.Stores.AnyAsync(
            x => x.CityEntityId == request.CityEntityId &&
                 x.Name.ToLower() == name.ToLower(),
            ct);

        if (exists)
            throw new MarketConflictException("Store with same name in this city already exists.");

        var store = new StoreEntity
        {
            Name = name,
            Contact = contact,
            Email = email,
            CityEntityId = request.CityEntityId,
            IsActive = true
        };

        ctx.Stores.Add(store);
        await ctx.SaveChangesAsync(ct);

        return store.Id;
    }
}