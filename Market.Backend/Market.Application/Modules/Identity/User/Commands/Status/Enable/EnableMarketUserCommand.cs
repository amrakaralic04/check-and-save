namespace Market.Application.Modules.Identity.User.Commands.Status.Enable
{
    public sealed class EnableMarketUserCommand : IRequest<Unit>
    {
        public required int Id { get; set; }
    }
}
