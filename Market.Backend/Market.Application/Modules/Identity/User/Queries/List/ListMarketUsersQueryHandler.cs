namespace Market.Application.Modules.Identity.User.Queries.List
{
    public sealed class ListMarketUsersQueryHandler(IAppDbContext ctx) : IRequestHandler<ListMarketUsersQuery, PageResult<ListMarketUsersQueryDto>>
    {
        public async Task<PageResult<ListMarketUsersQueryDto>> Handle(ListMarketUsersQuery request, CancellationToken ct)
        {
            var q = ctx.Users.AsNoTracking();

            if (request.IsEnabled.HasValue)
                q = q.Where(x => x.IsEnabled == request.IsEnabled.Value);

            if (request.IsAdmin.HasValue)
                q = q.Where(x => x.IsAdmin == request.IsAdmin.Value);

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var s = request.Search.Trim().ToLower();

                q = q.Where(x =>
                    x.Email.ToLower().Contains(s) ||
                    x.Firstname.ToLower().Contains(s) ||
                    x.Lastname.ToLower().Contains(s));
            }

            var pq = q.Select(x => new ListMarketUsersQueryDto
            {
                Id = x.Id,
                PublicId = x.PublicId,
                Firstname = x.Firstname,
                Lastname = x.Lastname,
                Email = x.Email,
                IsEnabled = x.IsEnabled,
                IsAdmin = x.IsAdmin,
                IsManager = x.IsManager,
                IsPublicUser = x.IsPublicUser,
                RegistrationDate = x.RegistrationDate
            });

            return await PageResult<ListMarketUsersQueryDto>
                .FromQueryableAsync(pq, request.Page, ct);
        }
    }
}