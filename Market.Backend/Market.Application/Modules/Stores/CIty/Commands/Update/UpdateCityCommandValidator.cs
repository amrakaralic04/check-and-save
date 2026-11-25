
namespace Market.Application.Modules.Stores.City.Commands.Update;

public sealed class UpdateCityCommandValidator
    : AbstractValidator<UpdateCityCommand>
{
    public UpdateCityCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name can be at most 100 characters long.");

        RuleFor(x => x.PostalCode)
            .GreaterThan(0).WithMessage("Postal code must be greater than zero.");
    }
}