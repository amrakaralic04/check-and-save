namespace Market.Application.Modules.Identity.Activity.Queries.List
{
    public sealed class ListActivitiesQueryDto
    {
        public int Id { get; set; }
        public int MarketUserEntityId { get; set; }
        public string ActivityType { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
