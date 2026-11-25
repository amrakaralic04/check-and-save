namespace Market.Application.Modules.Products.Price.Queries.GetById
{
    public sealed class GetPriceByIdQueryValidator : AbstractValidator<GetPriceByIdQuery>
    {
        public GetPriceByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be a positive value.");
        }
    }
}
