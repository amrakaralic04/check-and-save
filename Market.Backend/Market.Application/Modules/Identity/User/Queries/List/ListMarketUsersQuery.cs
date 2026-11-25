namespace Market.Application.Modules.Identity.User.Queries.List
{
    public sealed class ListMarketUsersQuery : BasePagedQuery<ListMarketUsersQueryDto>
    {
        public string? Search { get; set; } 
        public bool? IsEnabled { get; set; }
        public bool? IsAdmin { get; set; }
        public PageRequest? Page { get; internal set; }
    }
}
