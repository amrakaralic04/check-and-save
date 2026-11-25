using FluentValidation;
using Market.Domain.Entities.StoreEntities;

namespace Market.Application.Modules.Stores.Branches.Commands.Create;

public sealed class CreateBranchCommandValidator
    : AbstractValidator<CreateBranchCommand>
{
    public CreateBranchCommandValidator()
    {
        RuleFor(x => x.StoreEntityId)
            .GreaterThan(0);

        RuleFor(x => x.CityEntityId)
            .GreaterThan(0);

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(BranchEntity.Constraints.AddressMaxLength)
            .WithMessage($"Address can be at most {BranchEntity.Constraints.AddressMaxLength} characters long.");

        RuleFor(x => x.Contact)
            .NotEmpty().WithMessage("Contact is required.")
            .MaximumLength(BranchEntity.Constraints.ContactMaxLength)
            .WithMessage($"Contact can be at most {BranchEntity.Constraints.ContactMaxLength} characters long.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(BranchEntity.Constraints.EmailMaxLength)
            .WithMessage($"Email can be at most {BranchEntity.Constraints.EmailMaxLength} characters long.")
            .EmailAddress().WithMessage("Email is not in valid format.");
    }
}