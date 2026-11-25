namespace Market.Application.Modules.Identity.PublicUser.Queries.List;

public sealed class ListPublicUsersQuery : BasePagedQuery<ListPublicUsersQueryDto>
{
    public int? MarketUserEntityId { get; init; }
    public int? MinPoints { get; init; }
    public int? MaxPoints { get; init; }
}