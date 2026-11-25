namespace Market.Application.Modules.Identity.User.Commands.Update
{
    public sealed class UpdateMarketUserCommandHandler(IAppDbContext ctx) : IRequestHandler<UpdateMarketUserCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateMarketUserCommand request, CancellationToken ct)
        {
            var entity = await ctx.Users
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (entity is null)
                throw new MarketNotFoundException($"User (ID={request.Id}) nije pronađen.");

            var email = request.Email.Trim().ToLower();

            var emailTaken = await ctx.Users.AnyAsync(x =>
                x.Id != request.Id && x.Email.ToLower() == email, ct);

            if (emailTaken)
                throw new MarketConflictException("Email already exists.");

            entity.Firstname = request.Firstname.Trim();
            entity.Lastname = request.Lastname.Trim();
            entity.Email = email;

            entity.IsAdmin = request.IsAdmin;

            await ctx.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}