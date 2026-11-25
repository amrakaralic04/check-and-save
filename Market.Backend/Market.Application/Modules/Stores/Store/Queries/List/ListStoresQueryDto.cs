namespace Market.Application.Modules.Store.Stores.Queries.List;

public sealed class ListStoresQueryDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Contact { get; init; }
    public string Email { get; init; }
    public bool IsActive { get; init; }
    public int CityEntityId { get; init; }
}