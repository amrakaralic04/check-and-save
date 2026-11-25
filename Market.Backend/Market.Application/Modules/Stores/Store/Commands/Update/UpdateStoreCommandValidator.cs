using Market.Domain.Entities.StoreEntities;

namespace Market.Application.Modules.Stores.Store.Commands.Update;

public sealed class UpdateStoreCommandValidator
    : AbstractValidator<UpdateStoreCommand>
{
    public UpdateStoreCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.CityEntityId)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(StoreEntity.Constraints.NameMaxLength);

        RuleFor(x => x.Contact)
            .NotEmpty().WithMessage("Contact is required.")
            .MaximumLength(StoreEntity.Constraints.ContactMaxLength);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(StoreEntity.Constraints.EmailMaxLength)
            .EmailAddress().WithMessage("Email is not in valid format.");
    }
}