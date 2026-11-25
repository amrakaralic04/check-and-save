namespace Market.Application.Modules.Stores.Branches.Commands.Create;

public class CreateBranchCommand : IRequest<int>
{
    public int StoreEntityId { get; set; }
    public int CityEntityId { get; set; }

    public string Address { get; set; }
    public string Contact { get; set; }
    public string Email { get; set; }
}