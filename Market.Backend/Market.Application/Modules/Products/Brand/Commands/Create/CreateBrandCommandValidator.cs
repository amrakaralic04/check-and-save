
using Market.Domain.Entities.ProductEntities;

namespace Market.Application.Modules.Products.Brand.Commands.Create
{
    public sealed class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(BrandEntity.Constraints.NameMaxLength)
                .WithMessage($"Name can be at most {BrandEntity.Constraints.NameMaxLength} characters long.");

            RuleFor(x => x.Description)
                .MaximumLength(BrandEntity.Constraints.DescriptionMaxLength)
                .WithMessage($"Description can be at most {BrandEntity.Constraints.DescriptionMaxLength} characters long.");
        }
    }
}
