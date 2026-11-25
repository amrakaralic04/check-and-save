using Market.Application.Modules.Stores.City.Commands.Create;
using Market.Domain.Entities.StoreEntities;

namespace Market.Application.Modules.Stores.Cities.Commands.Create;

public sealed class CreateCityCommandValidator
    : AbstractValidator<CreateCityCommand>
{
    public CreateCityCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(CityEntity.Constraints.NameMaxLength)
            .WithMessage($"Name can be at most {CityEntity.Constraints.NameMaxLength} characters long.");

        RuleFor(x => x.PostalCode)
            .GreaterThan(0).WithMessage("Postal code must be greater than zero.");
    }
}