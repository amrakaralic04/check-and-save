namespace Market.Application.Modules.Products.Price.Commands.Delete
{
    public sealed class DeletePriceCommandValidator : AbstractValidator<DeletePriceCommand>
    {
        public DeletePriceCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
