using Market.Domain.Entities.ProductEntities;

namespace Market.Application.Modules.Products.Review.Commands.Update
{
    public sealed class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

            RuleFor(x => x.Rating)
                .InclusiveBetween(ReviewEntity.Constraints.RatingMin, ReviewEntity.Constraints.RatingMax);

            RuleFor(x => x.Comment)
                .MaximumLength(ReviewEntity.Constraints.CommentMaxLength)
                .When(x => x.Comment != null);

            RuleFor(x => x.Date).NotEmpty();
        }
    }
}
