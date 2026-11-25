namespace Market.Application.Modules.Auth.Commands.Login;


public sealed class LoginCommandDto
{
 
    public string AccessToken { get; set; }


    public string RefreshToken { get; set; }

   
    public DateTime ExpiresAtUtc { get; set; }

    public DateTime AccessTokenExpiresAtUtc { get; set; }

    public DateTime RefreshTokenExpiresAtUtc { get; set; }
}