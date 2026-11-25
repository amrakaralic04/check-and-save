namespace Market.Application.Modules.Identity.SecurityQuestion.Queries.GetById;

public sealed class GetSecQByIdQuery : IRequest<GetSecQByIdQueryDto>
{
    public int Id { get; set; }
}