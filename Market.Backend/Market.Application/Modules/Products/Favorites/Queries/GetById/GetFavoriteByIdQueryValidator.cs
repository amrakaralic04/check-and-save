namespace Market.Application.Modules.Products.Favorites.Queries.GetById
{
    public sealed class GetFavoriteByIdQueryValidator : AbstractValidator<GetFavoriteByIdQuery>
    {
        public GetFavoriteByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be a positive value.");
        }
    }
}
