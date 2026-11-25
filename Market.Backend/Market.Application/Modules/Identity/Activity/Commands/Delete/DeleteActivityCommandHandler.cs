namespace Market.Application.Modules.Identity.Activity.Commands.Delete
{
    public sealed class DeleteActivityCommandHandler(IAppDbContext ctx) : IRequestHandler<DeleteActivityCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteActivityCommand request, CancellationToken ct)
        {
            var entity = await ctx.Activities
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Activity (ID={request.Id}) nije pronađena.");

            ctx.Activities.Remove(entity);
            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
