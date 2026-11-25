using Market.Domain.Entities.ProductEntities;

namespace Market.Application.Modules.Products.Review.Commands.Create
{
    public sealed class CreateReviewCommandHandler(IAppDbContext ctx) : IRequestHandler<CreateReviewCommand, int>
    {
        public async Task<int> Handle(CreateReviewCommand request, CancellationToken ct)
        {
           
            var exists = await ctx.Reviews.AnyAsync(x =>
                x.PublicUserEntityId == request.PublicUserEntityId &&
                x.ProductEntityId == request.ProductEntityId, ct);

            if (exists)
                throw new MarketConflictException("Review already exists for this product.");

            var entity = new ReviewEntity
            {
                PublicUserEntityId = request.PublicUserEntityId,
                ProductEntityId = request.ProductEntityId,
                Rating = request.Rating,
                Comment = request.Comment?.Trim(),
                Date = request.Date
            };

            await ctx.Reviews.AddAsync(entity, ct);
            await ctx.SaveChangesAsync(ct);

            return entity.Id;
        }
    }
}
