using MediatR;
using Microsoft.EntityFrameworkCore;
using Market.Application.Abstractions;
using Market.Domain.Entities.StoreEntities;

namespace Market.Application.Modules.Stores.Branches.Commands.Update;

public sealed class UpdateBranchCommandHandler(IAppDbContext ctx)
    : IRequestHandler<UpdateBranchCommand, Unit>
{
    public async Task<Unit> Handle(UpdateBranchCommand request, CancellationToken ct)
    {
        var entity = await ctx.Branches
            .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

        if (entity is null)
            throw new MarketNotFoundException($"Branch (ID={request.Id}) not found.");

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
            x => x.Id != request.Id
              && x.StoreEntityId == request.StoreEntityId
              && x.CityEntityId == request.CityEntityId
              && x.Address == normalizedAddress,
            ct);

        if (exists)
            throw new MarketConflictException("Branch with same store, city and address already exists.");

        entity.StoreEntityId = request.StoreEntityId;
        entity.CityEntityId = request.CityEntityId;
        entity.Address = normalizedAddress;
        entity.Contact = normalizedContact;
        entity.Email = normalizedEmail;
        entity.IsActive = request.IsActive;

        await ctx.SaveChangesAsync(ct);

        return Unit.Value;
    }
}