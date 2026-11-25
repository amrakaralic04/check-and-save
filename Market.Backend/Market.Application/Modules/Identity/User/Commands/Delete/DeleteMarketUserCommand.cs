namespace Market.Application.Modules.Identity.User.Commands.Delete
{
    public sealed class DeleteMarketUserCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }

    }
}
