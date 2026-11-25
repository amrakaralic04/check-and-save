namespace Market.Application.Modules.Identity.User.Commands.Update;

public sealed class UpdateUserPubIdCommandDto
{
    public required int Id { get; init; }
    public required string Firstname { get; init; }
    public required string Lastname { get; init; }
    public required string Email { get; init; }
    public required bool IsAdmin { get; init; }
    public required bool IsManager { get; init; }
    public required bool IsPublicUser { get; init; }
    public required bool IsEnabled { get; init; }
    public required int AvatarLevel { get; init; }
}