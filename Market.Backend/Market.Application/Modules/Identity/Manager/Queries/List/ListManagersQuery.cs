namespace Market.Application.Modules.Identity.Manager.Queries.List
{
    public sealed class ListManagersQuery : BasePagedQuery<ListManagersQueryDto>
    {
        public int? StoreEntityId { get; set; }
        public int? MarketUserEntityId { get; set; }
        public PageRequest Page { get; internal set; }
    }
}
