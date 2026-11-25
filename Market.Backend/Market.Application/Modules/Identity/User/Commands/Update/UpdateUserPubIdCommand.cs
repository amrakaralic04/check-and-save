namespace Market.Application.Modules.Identity.User.Commands.Update;

public sealed class UpdateUserPubIdCommand : IRequest<UpdateUserPubIdCommandDto>
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int AvatarLevel { get; set; }
}