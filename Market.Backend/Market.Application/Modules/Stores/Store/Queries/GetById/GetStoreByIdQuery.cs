namespace Market.Application.Modules.Store.Stores.Queries.GetById;

public class GetStoreByIdQuery : IRequest<GetStoreByIdQueryDto>
{
    public int Id { get; set; }
}