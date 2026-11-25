namespace Market.Application.Modules.Stores.Branches.Queries.Map;

public sealed class BranchMapItemDto
{
    public int Id { get; init; }
    public string StoreName { get; init; }
    public string Address { get; init; }
    public string Contact { get; init; }
    public string Email { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}