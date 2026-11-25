namespace Market.Application.Modules.Identity.Activity.Commands.Create
{
    public sealed class CreateActivityCommand : IRequest<int>
    {
        public int MarketUserEntityId { get; set; }
        public required string ActivityType { get; set; }
        public required string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
