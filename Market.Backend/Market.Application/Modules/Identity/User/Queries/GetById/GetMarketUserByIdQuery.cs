namespace Market.Application.Modules.Identity.User.Queries.GetById
{
    public sealed class GetMarketUserByIdQuery : IRequest<GetMarketUserByIdQueryDto>
    {
        public int Id { get; set; }
    }
}
