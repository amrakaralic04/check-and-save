namespace Market.Application.Modules.Identity.Activity.Commands.Update
{
    public sealed class UpdateActivityCommandHandler(IAppDbContext ctx): IRequestHandler<UpdateActivityCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateActivityCommand request, CancellationToken ct)
        {
            var entity = await ctx.Activities
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
                throw new MarketNotFoundException($"Activity (ID={request.Id}) nije pronađena.");

            entity.ActivityType = request.ActivityType.Trim();
            entity.Description = request.Description.Trim();
            entity.Date = request.Date;

            await ctx.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
