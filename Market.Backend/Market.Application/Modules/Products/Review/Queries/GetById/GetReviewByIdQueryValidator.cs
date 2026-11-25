namespace Market.Application.Modules.Products.Review.Queries.GetById
{
    public sealed class GetReviewByIdQueryValidator : AbstractValidator<GetReviewByIdQuery>
    {
        public GetReviewByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be a positive value.");
        }
    }
}
