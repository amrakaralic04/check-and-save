namespace Market.Application.Modules.Identity.User.Commands.Create
{
    public sealed class CreateMarketUserCommandHandler(IAppDbContext ctx): IRequestHandler<CreateMarketUserCommand, int>
    {
        public async Task<int> Handle(CreateMarketUserCommand request, CancellationToken ct)
        {
            var email = request.Email.Trim().ToLower();

            var exists = await ctx.Users.AnyAsync(x => x.Email.ToLower() == email, ct);
            if (exists)
                throw new MarketConflictException("Email already exists.");

            var entity = new MarketUserEntity
            {
                Firstname = request.Firstname.Trim(),
                Lastname = request.Lastname.Trim(),
                Email = email,
                PasswordHash = request.PasswordHash,
                RegistrationDate = DateTime.UtcNow,
                IsAdmin = request.IsAdmin,
                IsManager = request.IsManager,
                IsPublicUser = request.IsPublicUser,
                IsEnabled = request.IsEnabled,
                TokenVersion = 0
            };

            await ctx.Users.AddAsync(entity, ct);
            await ctx.SaveChangesAsync(ct);

            return entity.Id;
        }
    }
}
