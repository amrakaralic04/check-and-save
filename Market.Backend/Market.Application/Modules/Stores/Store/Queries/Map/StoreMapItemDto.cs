namespace Market.Application.Modules.Store.Stores.Queries.Map;

public sealed class StoreMapItemDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Address { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}