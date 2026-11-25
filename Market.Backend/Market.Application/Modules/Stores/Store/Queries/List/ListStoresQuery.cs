namespace Market.Application.Modules.Store.Stores.Queries.List;

public sealed class ListStoresQuery : BasePagedQuery<ListStoresQueryDto>
{
    public int? CityEntityId { get; init; }
    public bool? OnlyActive { get; init; }
    public string? Search { get; init; }
}