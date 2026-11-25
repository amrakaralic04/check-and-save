namespace Market.Application.Modules.Products.Favorites.Commands.Update
{
    public sealed class UpdateFavoriteCommandValidator : AbstractValidator<UpdateFavoriteCommand>
    {
        public UpdateFavoriteCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.DateAdded).NotEmpty();
        }
    }
}
