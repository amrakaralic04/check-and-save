namespace Market.Application.Modules.Identity.PublicUser.Queries.GetById;

public class GetPublicUserByIdQueryDto
{
    public int Id { get; init; }
    public int MarketUserEntityId { get; init; }
    public int Points { get; init; }
    public int AvatarLevel { get; init; }
}