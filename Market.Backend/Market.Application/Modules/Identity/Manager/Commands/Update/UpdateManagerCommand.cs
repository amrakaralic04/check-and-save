namespace Market.Application.Modules.Identity.Manager.Commands.Update
{
    public sealed class UpdateManagerCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public int StoreEntityId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
