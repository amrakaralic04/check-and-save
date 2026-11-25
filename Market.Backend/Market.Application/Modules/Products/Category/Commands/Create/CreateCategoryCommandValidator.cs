
using Market.Domain.Entities.ProductEntities;

namespace Market.Application.Modules.Products.Category.Commands.Create
{
    public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(CategoryEntity.Constraints.NameMaxLength)
                .WithMessage($"Name can be at most {CategoryEntity.Constraints.NameMaxLength} characters long.");

            RuleFor(x => x.Description)
                .MaximumLength(CategoryEntity.Constraints.DescriptionMaxLength)
                .WithMessage($"Description can be at most {CategoryEntity.Constraints.DescriptionMaxLength} characters long.");
        }
    }
}
