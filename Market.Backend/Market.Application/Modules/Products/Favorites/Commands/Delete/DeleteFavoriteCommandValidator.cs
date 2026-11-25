namespace Market.Application.Modules.Products.Favorites.Commands.Delete
{
    public sealed class DeleteFavoriteCommandValidator : AbstractValidator<DeleteFavoriteCommand>
    {
        public DeleteFavoriteCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
