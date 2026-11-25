namespace Market.Application.Modules.Products.ItemComparison.Commands.Update
{
    public sealed class UpdateItemComparisonCommandValidator : AbstractValidator<UpdateItemComparisonCommand>
    {
        public UpdateItemComparisonCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.ProductId).GreaterThan(0);
        }

    }
}
