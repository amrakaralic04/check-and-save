namespace Market.Application.Modules.Identity.SecurityQuestion.Queries.List;

public sealed class ListSecQQuery : BasePagedQuery<ListSecQQueryDto>
{
    public int? MarketUserEntityId { get; init; }
    public string? Search { get; init; }
}