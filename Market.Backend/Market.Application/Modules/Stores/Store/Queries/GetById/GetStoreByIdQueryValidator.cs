namespace Market.Application.Modules.Store.Stores.Queries.GetById;

public sealed class GetStoreByIdQueryValidator : AbstractValidator<GetStoreByIdQuery>
{
    public GetStoreByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be a positive value.");
    }
}