namespace Market.Application.Modules.Products.Price.Commands.Create
{
    public sealed class CreatePriceCommandValidator : AbstractValidator<CreatePriceCommand>
    {
        public CreatePriceCommandValidator()
        {
            RuleFor(x => x.ProductEntityId).GreaterThan(0);
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0.");
            RuleFor(x => x.DateUpdated).NotEmpty();
        }
    }
}
