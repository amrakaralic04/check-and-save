namespace Market.Application.Modules.Products.Review.Commands.Update
{
    public sealed class UpdateReviewCommandHandler(IAppDbContext ctx) : IRequestHandler<UpdateReviewCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateReviewCommand request, CancellationToken ct)
        {
            var entity = await ctx.Reviews
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Review (ID={request.Id}) nije pronađen.");

            entity.Rating = request.Rating;
            entity.Comment = request.Comment?.Trim();
            entity.Date = request.Date;

            await ctx.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
