namespace Market.Application.Modules.Identity.SecurityQuestion.Command.Delete;

public sealed class DeleteSecQCommand : IRequest<Unit>
{
    public int Id { get; set; }
}