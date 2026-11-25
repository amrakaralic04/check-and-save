namespace Market.Application.Modules.Identity.Activity.Queries.List
{
    public sealed class ListActivitiesQuery : BasePagedQuery<ListActivitiesQueryDto>
    {
        public int? MarketUserEntityId { get; set; }
        public string? ActivityType { get; set; }
        public PageRequest Page { get; internal set; }
    }
}
