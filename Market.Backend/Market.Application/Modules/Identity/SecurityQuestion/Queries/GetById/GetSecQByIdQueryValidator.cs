namespace Market.Application.Modules.Identity.SecurityQuestion.Queries.GetById;

public sealed class GetSecQByIdQueryValidator
    : AbstractValidator<GetSecQByIdQuery>
{
    public GetSecQByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}