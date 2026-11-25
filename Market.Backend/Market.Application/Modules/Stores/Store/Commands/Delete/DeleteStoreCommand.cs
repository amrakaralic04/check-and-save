namespace Market.Application.Modules.Store.Stores.Commands.Delete;

public class DeleteStoreCommand : IRequest<Unit>
{
    public int Id { get; set; }
}