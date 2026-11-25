namespace Market.Application.Modules.Identity.PublicUser.Queries.GetById;

public class GetPublicUserByIdQuery : IRequest<GetPublicUserByIdQueryDto>
{
    public int Id { get; set; }
}