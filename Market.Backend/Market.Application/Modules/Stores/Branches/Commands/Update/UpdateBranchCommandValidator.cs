using FluentValidation;
using Market.Domain.Entities.StoreEntities;

namespace Market.Application.Modules.Stores.Branches.Commands.Update;

public sealed class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
{
    public UpdateBranchCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.StoreEntityId).GreaterThan(0);
        RuleFor(x => x.CityEntityId).GreaterThan(0);

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(BranchEntity.Constraints.AddressMaxLength);

        RuleFor(x => x.Contact)
            .NotEmpty().WithMessage("Contact is required.")
            .MaximumLength(BranchEntity.Constraints.ContactMaxLength);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(BranchEntity.Constraints.EmailMaxLength)
            .EmailAddress().WithMessage("Email is not in valid format.");
    }
}