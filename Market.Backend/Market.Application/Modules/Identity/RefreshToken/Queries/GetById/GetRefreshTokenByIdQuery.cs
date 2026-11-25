namespace Market.Application.Modules.Identity.RefreshToken.Queries.GetById;

public sealed class GetRefreshTokenByIdQuery : IRequest<GetRefreshTokenByIdQueryDto>
{
    public int Id { get; set; }
}