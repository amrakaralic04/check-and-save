namespace Market.Application.Modules.Products.Brand.Commands.Delete
{
    public sealed class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
    {
        public DeleteBrandCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
