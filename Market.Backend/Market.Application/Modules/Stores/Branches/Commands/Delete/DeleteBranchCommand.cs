namespace Market.Application.Modules.Stores.Branches.Commands.Delete;

public class DeleteBranchCommand : IRequest<Unit>
{
    public int Id { get; set; }
}