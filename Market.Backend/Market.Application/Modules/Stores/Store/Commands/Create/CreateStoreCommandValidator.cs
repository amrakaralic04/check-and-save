using Market.Domain.Entities.StoreEntities;

namespace Market.Application.Modules.Stores.Store.Commands.Create;

public sealed class CreateStoreCommandValidator
    : AbstractValidator<CreateStoreCommand>
{
    public CreateStoreCommandValidator()
    {
        RuleFor(x => x.CityEntityId)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(StoreEntity.Constraints.NameMaxLength)
            .WithMessage($"Name can be at most {StoreEntity.Constraints.NameMaxLength} characters long.");

        RuleFor(x => x.Contact)
            .NotEmpty().WithMessage("Contact is required.")
            .MaximumLength(StoreEntity.Constraints.ContactMaxLength)
            .WithMessage($"Contact can be at most {StoreEntity.Constraints.ContactMaxLength} characters long.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(StoreEntity.Constraints.EmailMaxLength)
            .WithMessage($"Email can be at most {StoreEntity.Constraints.EmailMaxLength} characters long.")
            .EmailAddress().WithMessage("Email is not in valid format.");
    }
}