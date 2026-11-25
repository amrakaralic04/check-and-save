using Market.Domain.Entities.StoreEntities;

namespace Market.Application.Modules.Stores.Branches.Commands.Create;

public class CreateBranchCommandHandler(IAppDbContext ctx)
    : IRequestHandler<CreateBranchCommand, int>
{
    public async Task<int> Handle(CreateBranchCommand request, CancellationToken ct)
    {
        var storeExists = await ctx.Stores
            .AnyAsync(x => x.Id == request.StoreEntityId, ct);

        if (!storeExists)
            throw new MarketNotFoundException($"Store (ID={request.StoreEntityId}) not found.");

        var cityExists = await ctx.Cities
            .AnyAsync(x => x.Id == request.CityEntityId, ct);

        if (!cityExists)
            throw new MarketNotFoundException($"City (ID={request.CityEntityId}) not found.");

        var normalizedAddress = request.Address.Trim();
        var normalizedContact = request.Contact.Trim();
        var normalizedEmail = request.Email.Trim();

        var exists = await ctx.Branches.AnyAsync(
            x => x.StoreEntityId == request.StoreEntityId
              && x.CityEntityId == request.CityEntityId
              && x.Address == normalizedAddress,
            ct);

        if (exists)
            throw new MarketConflictException("Branch with same store, city and address already exists.");

        var branch = new BranchEntity
        {
            StoreEntityId = request.StoreEntityId,
            CityEntityId = request.CityEntityId,
            Address = normalizedAddress,
            Contact = normalizedContact,
            Email = normalizedEmail,
            IsActive = true
        };

        ctx.Branches.Add(branch);
        await ctx.SaveChangesAsync(ct);

        return branch.Id;
    }
}