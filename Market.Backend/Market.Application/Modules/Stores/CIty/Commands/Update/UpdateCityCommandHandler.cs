namespace Market.Application.Modules.Stores.City.Commands.Update;

public sealed class UpdateCityCommandHandler(IAppDbContext ctx)
    : IRequestHandler<UpdateCityCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCityCommand request, CancellationToken ct)
    {
        var entity = await ctx.Cities
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(ct);

        if (entity is null)
            throw new MarketNotFoundException($"Grad (ID={request.Id}) nije pronađen.");

        var normalizedName = request.Name.Trim();

        var exists = await ctx.Cities
            .AnyAsync(x =>
                x.Id != request.Id
                && x.Name.ToLower() == normalizedName.ToLower()
                && x.PostalCode == request.PostalCode,
                ct);

        if (exists)
            throw new MarketConflictException("City with the same name and postal code already exists.");

        entity.Name = normalizedName;
        entity.PostalCode = request.PostalCode;

        await ctx.SaveChangesAsync(ct);

        return Unit.Value;
    }
}