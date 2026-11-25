namespace Market.Application.Modules.Products.ItemComparison.Commands.Delete
{
    public sealed class DeleteItemComparisonCommandValidator : AbstractValidator<DeleteItemComparisonCommand>
    {
        public DeleteItemComparisonCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
