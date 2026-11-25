namespace Market.Application.Modules.Stores.Branches.Queries.List;

public sealed class ListBranchesQueryDto
{
    public int Id { get; init; }
    public int StoreEntityId { get; init; }
    public int CityEntityId { get; init; }

    public string Address { get; init; }
    public string Contact { get; init; }
    public string Email { get; init; }

    public bool IsActive { get; init; }
}