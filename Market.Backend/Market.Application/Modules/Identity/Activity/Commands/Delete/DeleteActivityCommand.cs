namespace Market.Application.Modules.Identity.Activity.Commands.Delete
{
    public sealed class DeleteActivityCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
