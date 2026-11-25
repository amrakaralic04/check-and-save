namespace Market.Application.Modules.Identity.SecurityQuestion.Queries.GetById;

public sealed class GetSecQByIdQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetSecQByIdQuery, GetSecQByIdQueryDto>
{
    public async Task<GetSecQByIdQueryDto> Handle(
        GetSecQByIdQuery request, CancellationToken ct)
    {
        var entity = await ctx.SecurityQuestions
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new GetSecQByIdQueryDto
            {
                Id = x.Id,
                MarketUserEntityId = x.MarketUserEntityId,
                Question = x.Question,
                Answer = x.Answer
            })
            .FirstOrDefaultAsync(ct);

        if (entity is null)
            throw new MarketNotFoundException($"Security question (ID={request.Id}) not found.");

        return entity;
    }
}