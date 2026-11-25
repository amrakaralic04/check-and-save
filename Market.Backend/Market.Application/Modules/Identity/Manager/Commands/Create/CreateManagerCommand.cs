namespace Market.Application.Modules.Identity.Manager.Commands.Create
{
    public sealed class CreateManagerCommand : IRequest<int>
    {
        public int MarketUserEntityId { get; set; }
        public int StoreEntityId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
