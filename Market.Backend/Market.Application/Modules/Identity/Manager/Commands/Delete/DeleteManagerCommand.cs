namespace Market.Application.Modules.Identity.Manager.Commands.Delete
{
    public sealed class DeleteManagerCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
