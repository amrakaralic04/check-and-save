namespace Market.Application.Modules.Identity.PublicUser.Commands.Delete;

public class DeletePublicUserCommand : IRequest<Unit>
{
    public int Id { get; set; }
}