namespace Market.Application.Modules.Identity.Manager.Queries.GetById
{
    public sealed class GetManagerByIdQuery : IRequest<GetManagerByIdQueryDto>
    {
        public int Id { get; set; }
    }
}
