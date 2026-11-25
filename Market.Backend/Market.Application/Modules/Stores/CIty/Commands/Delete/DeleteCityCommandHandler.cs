

namespace Market.Application.Modules.Stores.City.Commands.Delete;

public class DeleteCityCommandHandler(IAppDbContext context)
    : IRequestHandler<DeleteCityCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var city = await context.Cities
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (city is null)
            throw new MarketNotFoundException("Grad nije pronađen.");

        context.Cities.Remove(city);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}