namespace Market.Application.Modules.Identity.PublicUser.Commands.Update;

public sealed class UpdatePublicUserCommand : IRequest<Unit>
{
    [JsonIgnore]
    public int Id { get; set; }

    public int Points { get; set; }
    public int AvatarLevel { get; set; }
}