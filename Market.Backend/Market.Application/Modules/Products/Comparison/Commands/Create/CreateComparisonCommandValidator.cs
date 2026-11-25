namespace Market.Application.Modules.Products.Comparison.Commands.Create
{
    public sealed class CreateComparisonCommandValidator : AbstractValidator<CreateComparisonCommand>
    {
        public CreateComparisonCommandValidator()
        {
            RuleFor(x => x.CustomerEntityId)
                .GreaterThan(0)
                .WithMessage("CustomerEntityId must be a positive value.");

            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Date is required.");
        }
    }
}
