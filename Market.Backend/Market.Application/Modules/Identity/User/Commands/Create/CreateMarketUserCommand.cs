namespace Market.Application.Modules.Identity.User.Commands.Create
{
    public sealed class CreateMarketUserCommand : IRequest<int>
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public bool IsAdmin { get; set; }
        public bool IsManager { get; set; }
        public bool IsPublicUser { get; set; }

        public bool IsEnabled { get; set; } = true;
    }
}
