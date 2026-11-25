namespace Market.Application.Modules.Products.Brand.Commands.Update
{
    public sealed class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name can be at most 200 characters long.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description can be at most 1000 characters long.")
                .When(x => x.Description != null);
        }

    }
}
