namespace Market.Application.Modules.Identity.RefreshToken.Queries.GetById;

public sealed class GetRefreshTokenByIdQueryValidator
    : AbstractValidator<GetRefreshTokenByIdQuery>
{
    public GetRefreshTokenByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}