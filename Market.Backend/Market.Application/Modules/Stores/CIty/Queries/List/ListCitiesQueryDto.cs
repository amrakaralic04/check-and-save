namespace Market.Application.Modules.Stores.City.Queries.List;

public sealed class ListCitiesQueryDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int PostalCode { get; init; }
}