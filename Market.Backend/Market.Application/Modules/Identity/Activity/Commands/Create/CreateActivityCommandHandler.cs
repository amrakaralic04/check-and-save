namespace Market.Application.Modules.Identity.Activity.Commands.Create
{
    public sealed class CreateActivityCommandHandler(IAppDbContext ctx) : IRequestHandler<CreateActivityCommand, int>
    {
        public async Task<int> Handle(CreateActivityCommand request, CancellationToken ct)
        {
            var entity = new ActivityEntity
            {
                MarketUserEntityId = request.MarketUserEntityId,
                ActivityType = request.ActivityType.Trim(),
                Description = request.Description.Trim(),
                Date = request.Date
            };

            await ctx.Activities.AddAsync(entity, ct);
            await ctx.SaveChangesAsync(ct);

            return entity.Id;
        }
    }
}
