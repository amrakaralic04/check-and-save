namespace Market.Application.Modules.Products.Review.Commands.Delete
{
    public sealed class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
    {
        public DeleteReviewCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
