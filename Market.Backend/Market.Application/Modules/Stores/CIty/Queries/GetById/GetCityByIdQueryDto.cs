namespace Market.Application.Modules.Stores.City.Queries.GetById;

public class GetCityByIdQueryDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int PostalCode { get; init; }
}