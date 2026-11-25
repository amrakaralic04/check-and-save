namespace Market.Application.Modules.Stores.City.Commands.Delete;

public class DeleteCityCommand : IRequest<Unit>
{
    public int Id { get; set; }
}