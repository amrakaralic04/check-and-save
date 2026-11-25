namespace Market.Application.Modules.Auth.Commands.Register;

public sealed class RegisterCommand : IRequest<int>
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Gender { get; init; } = string.Empty;
}