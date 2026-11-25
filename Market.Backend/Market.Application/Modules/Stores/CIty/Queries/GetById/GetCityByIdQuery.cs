namespace Market.Application.Modules.Stores.City.Queries.GetById;

public class GetCityByIdQuery : IRequest<GetCityByIdQueryDto>
{
    public int Id { get; set; }
}