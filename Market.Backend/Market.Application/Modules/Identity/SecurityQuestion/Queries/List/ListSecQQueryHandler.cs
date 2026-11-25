namespace Market.Application.Modules.Identity.SecurityQuestion.Queries.List;

public sealed class ListSecQQueryHandler(IAppDbContext ctx)
    : IRequestHandler<ListSecQQuery, PageResult<ListSecQQueryDto>>
{
    public async Task<PageResult<ListSecQQueryDto>> Handle(
        ListSecQQuery request, CancellationToken ct)
    {
        var q = ctx.SecurityQuestions.AsNoTracking();

        if (request.MarketUserEntityId is not null)
            q = q.Where(x => x.MarketUserEntityId == request.MarketUserEntityId);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();
            q = q.Where(x => x.Question.Contains(search));
        }

        var projected = q
            .OrderBy(x => x.MarketUserEntityId)
            .ThenBy(x => x.Id)
            .Select(x => new ListSecQQueryDto
            {
                Id = x.Id,
                MarketUserEntityId = x.MarketUserEntityId,
                Question = x.Question,
                Answer = x.Answer
            });

        return await PageResult<ListSecQQueryDto>.FromQueryableAsync(
            projected,
            request.Paging,
            ct);
    }
}