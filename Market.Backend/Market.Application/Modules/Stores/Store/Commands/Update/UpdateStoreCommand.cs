namespace Market.Application.Modules.Stores.Store.Commands.Update;

public sealed class UpdateStoreCommand : IRequest<Unit>
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Name { get; set; }
    public string Contact { get; set; }
    public string Email { get; set; }

    public int CityEntityId { get; set; }
    public bool IsActive { get; set; }
}