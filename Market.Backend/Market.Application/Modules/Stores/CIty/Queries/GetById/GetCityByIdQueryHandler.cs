namespace Market.Application.Modules.Stores.City.Queries.GetById;

public class GetCityByIdQueryHandler(IAppDbContext context)
    : IRequestHandler<GetCityByIdQuery, GetCityByIdQueryDto>
{
    public async Task<GetCityByIdQueryDto> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await context.Cities
            .AsNoTracking()
            .Where(c => c.Id == request.Id)
            .Select(x => new GetCityByIdQueryDto
            {
                Id = x.Id,
                Name = x.Name,
                PostalCode = x.PostalCode
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (city is null)
        {
            throw new MarketNotFoundException($"City with Id {request.Id} not found.");
        }

        return city;
    }
}