namespace Market.Application.Modules.Products.Comparison.Commands.Update
{
    public sealed class UpdateComparisonCommandValidator : AbstractValidator<UpdateComparisonCommand> 
    {
        public UpdateComparisonCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Date).NotEmpty();
        }
    }
}
