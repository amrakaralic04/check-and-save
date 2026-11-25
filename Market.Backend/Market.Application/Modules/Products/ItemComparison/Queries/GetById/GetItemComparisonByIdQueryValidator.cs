namespace Market.Application.Modules.Products.ItemComparison.Queries.GetById
{
    public sealed class GetItemComparisonByIdQueryValidator : AbstractValidator<GetItemComparisonByIdQuery>
    {
        public GetItemComparisonByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be a positive value.");
        }
    }
}
