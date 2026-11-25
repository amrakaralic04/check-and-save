namespace Market.Application.Modules.Stores.Store.Commands.Create;

public class CreateStoreCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Contact { get; set; }
    public string Email { get; set; }

    public int CityEntityId { get; set; }
}