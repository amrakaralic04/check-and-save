namespace Market.Application.Modules.Identity.User.Commands.Update;

public sealed class UpdateUserPubIdCommandHandler(IAppDbContext ctx)
    : IRequestHandler<UpdateUserPubIdCommand, UpdateUserPubIdCommandDto>
{
    public async Task<UpdateUserPubIdCommandDto> Handle(UpdateUserPubIdCommand request, CancellationToken ct)
    {
        var entity = await ctx.Users
             .Include(x => x.PublicUserEntity)
            .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

        if (entity is null)
            throw new MarketNotFoundException($"User (Id={request.Id}) nije pronađen.");

        var email = request.Email.Trim().ToLowerInvariant();

        var emailTaken = await ctx.Users.AnyAsync(x =>
            x.Id != entity.Id && x.Email.ToLower() == email, ct);

        if (emailTaken)
            throw new MarketConflictException("Email already exists.");

        entity.Firstname = request.Firstname.Trim();
        entity.Lastname = request.Lastname.Trim();
        entity.Email = email;

        if (entity.PublicUserEntity is not null)
        {
            entity.PublicUserEntity.AvatarLevel = request.AvatarLevel;
        }


        await ctx.SaveChangesAsync(ct);

        return new UpdateUserPubIdCommandDto
        {
            Id = entity.Id,
            Firstname = entity.Firstname,
            Lastname = entity.Lastname,
            Email = entity.Email,
            IsAdmin = entity.IsAdmin,
            IsManager = entity.IsManager,
            IsPublicUser = entity.IsPublicUser,
            IsEnabled = entity.IsEnabled,
            AvatarLevel = entity.PublicUserEntity?.AvatarLevel ?? 1
        };
    }
}