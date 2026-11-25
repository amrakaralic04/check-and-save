namespace Market.Application.Modules.Products.ItemComparison.Commands.Create
{
    public sealed class CreateItemComparisonCommandValidator : AbstractValidator<CreateItemComparisonCommand>
    {
        public CreateItemComparisonCommandValidator()
        {
            RuleFor(x => x.ComparisonEntityId).GreaterThan(0);
            RuleFor(x => x.ProductId).GreaterThan(0);
        }
    }
}
