namespace Market.Application.Modules.Identity.Activity.Commands.Update
{
    public sealed class UpdateActivityCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public required string ActivityType { get; set; }
        public required string Description { get; set; }
        public DateTime Date { get; set; }

    }
}
