using Market.Application.Modules.Auth.Commands.Register;

public sealed class RegisterCommandHandler(
    IAppDbContext ctx,
    IPasswordHasher<MarketUserEntity> hasher)
    : IRequestHandler<RegisterCommand, int>
{
    public async Task<int> Handle(RegisterCommand request, CancellationToken ct)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var exists = await ctx.Users.AnyAsync(x => x.Email.ToLower() == email, ct);
        if (exists)
            throw new MarketConflictException("Email already exists.");

        var user = new MarketUserEntity
        {
            Firstname = request.FirstName.Trim(),
            Lastname = request.LastName.Trim(),
            Email = email,
            RegistrationDate = DateTime.UtcNow,
            IsAdmin = false,
            IsManager = false,
            IsPublicUser = true,
            IsEnabled = true,
            TokenVersion = 0
        };

        user.PasswordHash = hasher.HashPassword(user, request.Password);

        var publicUser = new PublicUserEntity
        {
            MarketUserEntity = user,
            Points = 0,
            AvatarLevel = request.Gender.Trim().Equals("male", StringComparison.OrdinalIgnoreCase) ? 1 : 2
        };

        await ctx.PublicUsers.AddAsync(publicUser, ct);
        await ctx.SaveChangesAsync(ct);

        return user.Id;
    }
}