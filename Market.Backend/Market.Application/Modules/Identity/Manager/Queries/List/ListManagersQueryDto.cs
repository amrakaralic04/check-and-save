namespace Market.Application.Modules.Identity.Manager.Queries.List
{
    public sealed class ListManagersQueryDto
    {
        public int Id { get; set; }
        public int MarketUserEntityId { get; set; }
        public int StoreEntityId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
