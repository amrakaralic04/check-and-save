namespace Market.Application.Modules.Identity.User.Queries.GetById
{
    public sealed class GetMarketUserByIdQueryHandler(IAppDbContext ctx): IRequestHandler<GetMarketUserByIdQuery, GetMarketUserByIdQueryDto>
    {
        public async Task<GetMarketUserByIdQueryDto> Handle(GetMarketUserByIdQuery request, CancellationToken ct)
        {
            var user = await ctx.Users
                .Where(x => x.Id == request.Id)
                .Select(x => new GetMarketUserByIdQueryDto
                {
                    Id = x.Id,
             
                    Firstname = x.Firstname,
                    Lastname = x.Lastname,
                    Email = x.Email,
                    RegistrationDate = x.RegistrationDate,
                    IsAdmin = x.IsAdmin,
                    IsManager = x.IsManager,
                    IsPublicUser = x.IsPublicUser,
                    IsEnabled = x.IsEnabled,
                    TokenVersion = x.TokenVersion,
                    AvatarLevel = x.PublicUserEntity != null ? x.PublicUserEntity.AvatarLevel : 1
                })
                .FirstOrDefaultAsync(ct);

            if (user is null)
                throw new MarketNotFoundException($"User (ID={request.Id}) nije pronađen.");

            return user;
        }
    }
}
