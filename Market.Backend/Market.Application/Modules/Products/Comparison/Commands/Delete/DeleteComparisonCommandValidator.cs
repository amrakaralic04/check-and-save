namespace Market.Application.Modules.Products.Comparison.Commands.Delete
{
    public sealed class DeleteComparisonCommandValidator : AbstractValidator<DeleteComparisonCommand>
    {
        public DeleteComparisonCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
