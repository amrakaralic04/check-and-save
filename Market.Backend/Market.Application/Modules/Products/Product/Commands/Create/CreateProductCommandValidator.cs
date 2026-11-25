using Market.Domain.Entities.ProductEntities;

namespace Market.Application.Modules.Products.Product.Commands.Create
{
    public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.StoreEntityId).GreaterThan(0);
            RuleFor(x => x.BranchEntityId).GreaterThan(0);
            RuleFor(x => x.CategoryEntityId).GreaterThan(0);
            RuleFor(x => x.BrandEntityId).GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(ProductEntity.Constraints.NameMaxLength);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(ProductEntity.Constraints.DescriptionMaxLength);

            RuleFor(x => x.ImageURL)
                .NotEmpty()
                .MaximumLength(ProductEntity.Constraints.ImageUrlMaxLength);

            RuleFor(x => x.DateAdded).NotEmpty();
        }
    }
}
