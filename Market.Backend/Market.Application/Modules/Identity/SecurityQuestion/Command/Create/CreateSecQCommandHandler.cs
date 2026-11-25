namespace Market.Application.Modules.Identity.SecurityQuestion.Command.Create;

public sealed class CreateSecQCommandHandler(IAppDbContext ctx)
    : IRequestHandler<CreateSecQCommand, int>
{
    public async Task<int> Handle(CreateSecQCommand request, CancellationToken ct)
    {
        var exists = await ctx.SecurityQuestions
            .AnyAsync(x => x.MarketUserEntityId == request.MarketUserEntityId, ct);

        if (exists)
            throw new MarketConflictException("Security question for this user already exists.");

        var normalizedQuestion = request.Question.Trim();
        var normalizedAnswer = request.Answer.Trim();

        var entity = new SecurityQuestionEntity
        {
            MarketUserEntityId = request.MarketUserEntityId,
            Question = normalizedQuestion,
            Answer = normalizedAnswer
        };

        ctx.SecurityQuestions.Add(entity);
        await ctx.SaveChangesAsync(ct);

        return entity.Id;
    }
}