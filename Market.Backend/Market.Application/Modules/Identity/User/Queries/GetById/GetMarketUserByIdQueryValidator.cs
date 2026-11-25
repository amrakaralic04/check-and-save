namespace Market.Application.Modules.Identity.User.Queries.GetById
{
    public sealed class GetMarketUserByIdQueryValidator : AbstractValidator<GetMarketUserByIdQuery>
    {
        public GetMarketUserByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be a positive value.");
        }
    }
}
