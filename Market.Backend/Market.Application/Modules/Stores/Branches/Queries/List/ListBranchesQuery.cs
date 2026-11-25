namespace Market.Application.Modules.Stores.Branches.Queries.List;

public sealed class ListBranchesQuery : BasePagedQuery<ListBranchesQueryDto>
{
    public int? StoreEntityId { get; init; }
    public int? CityEntityId { get; init; }
    public bool? OnlyActive { get; init; }
    public string? Search { get; init; }
}