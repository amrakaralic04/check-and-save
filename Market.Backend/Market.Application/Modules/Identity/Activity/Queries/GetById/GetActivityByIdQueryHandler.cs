namespace Market.Application.Modules.Identity.Activity.Queries.GetById
{
    public sealed class GetActivityByIdQueryHandler(IAppDbContext ctx) : IRequestHandler<GetActivityByIdQuery, GetActivityByIdQueryDto>
    {
        public async Task<GetActivityByIdQueryDto> Handle(GetActivityByIdQuery request, CancellationToken ct)
        {
            var activity = await ctx.Activities
                .Where(x => x.Id == request.Id)
                .Select(x => new GetActivityByIdQueryDto
                {
                    Id = x.Id,
                    MarketUserEntityId = x.MarketUserEntityId,
                    ActivityType = x.ActivityType,
                    Description = x.Description,
                    Date = x.Date
                })
                .FirstOrDefaultAsync(ct);

            if (activity is null)
                throw new MarketNotFoundException($"Activity (ID={request.Id}) nije pronađena.");

            return activity;
        }
    }
}
