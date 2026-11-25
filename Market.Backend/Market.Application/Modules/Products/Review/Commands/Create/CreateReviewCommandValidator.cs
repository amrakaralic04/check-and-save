using Market.Domain.Entities.ProductEntities;

namespace Market.Application.Modules.Products.Review.Commands.Create
{
    public sealed class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.PublicUserEntityId).GreaterThan(0);
            RuleFor(x => x.ProductEntityId).GreaterThan(0);

            RuleFor(x => x.Rating)
                .InclusiveBetween(ReviewEntity.Constraints.RatingMin, ReviewEntity.Constraints.RatingMax)
                .WithMessage($"Rating must be between {ReviewEntity.Constraints.RatingMin} and {ReviewEntity.Constraints.RatingMax}.");

            RuleFor(x => x.Comment)
                .MaximumLength(ReviewEntity.Constraints.CommentMaxLength)
                .When(x => x.Comment != null);

            RuleFor(x => x.Date).NotEmpty();
        }
    }
}
