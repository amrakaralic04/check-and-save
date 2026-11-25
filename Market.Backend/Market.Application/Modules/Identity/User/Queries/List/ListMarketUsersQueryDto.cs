namespace Market.Application.Modules.Identity.User.Queries.List
{
    public sealed class ListMarketUsersQueryDto
    {
        public int Id { get; set; }
        public string PublicId { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsEnabled { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsManager { get; set; }
        public bool IsPublicUser { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
