namespace Market.Application.Modules.Identity.User.Commands.Status.Disable
{
    public sealed class DisableMarketUserCommand : IRequest<Unit>
    {
        public required int Id { get; set; }
    }
}
