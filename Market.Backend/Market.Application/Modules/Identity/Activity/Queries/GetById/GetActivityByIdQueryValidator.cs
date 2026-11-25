namespace Market.Application.Modules.Identity.Activity.Queries.GetById
{
    public sealed class GetActivityByIdQueryValidator : AbstractValidator<GetActivityByIdQuery>
    {
        public GetActivityByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be a positive value.");
        }
    }
}
