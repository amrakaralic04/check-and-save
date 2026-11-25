namespace Market.Application.Modules.Identity.Manager.Queries.GetById
{
    public sealed class GetManagerByIdQueryValidator : AbstractValidator<GetManagerByIdQuery>
    {
        public GetManagerByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be a positive value.");
        }
    }
}
