namespace Market.Application.Modules.Identity.Activity.Queries.GetById
{
    public sealed class GetActivityByIdQuery : IRequest<GetActivityByIdQueryDto>
    {
        public int Id { get; set; }
    }
}
