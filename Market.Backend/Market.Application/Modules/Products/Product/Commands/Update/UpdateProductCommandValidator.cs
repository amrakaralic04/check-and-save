using Market.Domain.Entities.ProductEntities;

namespace Market.Application.Modules.Products.Product.Commands.Update
{
    public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

            RuleFor(x => x.StoreEntityId).GreaterThan(0);
            RuleFor(x => x.BranchEntityId).GreaterThan(0);
            RuleFor(x => x.CategoryEntityId).GreaterThan(0);
            RuleFor(x => x.BrandEntityId).GreaterThan(0);

            RuleFor(x => x.Name).NotEmpty().MaximumLength(ProductEntity.Constraints.NameMaxLength);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(ProductEntity.Constraints.DescriptionMaxLength);
            RuleFor(x => x.ImageURL).NotEmpty().MaximumLength(ProductEntity.Constraints.ImageUrlMaxLength);
        }
    }
}
