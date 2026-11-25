namespace Market.Application.Modules.Identity.PublicUser.Queries.GetById;

public sealed class GetPublicUserByIdQueryValidator : AbstractValidator<GetPublicUserByIdQuery>
{
    public GetPublicUserByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be a positive value.");
    }
}