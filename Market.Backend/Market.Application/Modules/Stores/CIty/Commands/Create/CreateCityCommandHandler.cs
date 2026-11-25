using Market.Application.Modules.Stores.City.Commands.Create;
using Market.Domain.Entities.StoreEntities;

namespace Market.Application.Modules.Stores.Cities.Commands.Create;

public class CreateCityCommandHandler(IAppDbContext context)
    : IRequestHandler<CreateCityCommand, int>
{
    public async Task<int> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var normalizedName = request.Name?.Trim();

        if (string.IsNullOrWhiteSpace(normalizedName))
            throw new ValidationException("Name is required.");

        bool exists = await context.Cities
            .AnyAsync(x => x.Name == normalizedName && x.PostalCode == request.PostalCode,
                      cancellationToken);

        if (exists)
            throw new MarketConflictException("City with the same name and postal code already exists.");

        var city = new CityEntity
        {
            Name = normalizedName,
            PostalCode = request.PostalCode
        };

        context.Cities.Add(city);
        await context.SaveChangesAsync(cancellationToken);

        return city.Id;
    }
}