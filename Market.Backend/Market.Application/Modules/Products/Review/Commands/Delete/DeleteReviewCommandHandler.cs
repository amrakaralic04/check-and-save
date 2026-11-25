namespace Market.Application.Modules.Products.Review.Commands.Delete
{
    public sealed class DeleteReviewCommandHandler(IAppDbContext ctx) : IRequestHandler<DeleteReviewCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken ct)
        {
            var entity = await ctx.Reviews
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Review (ID={request.Id}) nije pronađen.");

            ctx.Reviews.Remove(entity);
            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
