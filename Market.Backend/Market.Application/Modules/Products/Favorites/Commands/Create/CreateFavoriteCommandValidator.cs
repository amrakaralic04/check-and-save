namespace Market.Application.Modules.Products.Favorites.Commands.Create
{
    public sealed class CreateFavoriteCommandValidator : AbstractValidator<CreateFavoriteCommand>
    {
        public CreateFavoriteCommandValidator()
        {
            RuleFor(x => x.PublicUserEntityId).GreaterThan(0);
            RuleFor(x => x.ProductEntityId).GreaterThan(0);
            RuleFor(x => x.DateAdded).NotEmpty();
        }
    }
}
