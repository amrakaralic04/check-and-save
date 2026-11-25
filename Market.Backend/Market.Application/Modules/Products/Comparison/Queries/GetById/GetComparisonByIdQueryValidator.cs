namespace Market.Application.Modules.Products.Comparison.Queries.GetById
{
    public sealed class GetComparisonByIdQueryValidator : AbstractValidator<GetComparisonByIdQuery>
    {
        public GetComparisonByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id must be a positive value.");
        }
    }
}
