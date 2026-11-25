namespace Market.Application.Modules.Stores.City.Queries.List;

public sealed class ListCitiesQuery : BasePagedQuery<ListCitiesQueryDto>
{
    public string? Search { get; init; }
    public int? PostalCode { get; init; }
}