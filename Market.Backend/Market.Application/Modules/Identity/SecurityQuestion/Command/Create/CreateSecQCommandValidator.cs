namespace Market.Application.Modules.Identity.SecurityQuestion.Command.Create;

public sealed class CreateSecQCommandValidator
    : AbstractValidator<CreateSecQCommand>
{
    public CreateSecQCommandValidator()
    {
        RuleFor(x => x.MarketUserEntityId)
            .GreaterThan(0);

        RuleFor(x => x.Question)
            .NotEmpty().WithMessage("Question is required.")
            .MaximumLength(SecurityQuestionEntity.Constraints.QuestionMaxLength);

        RuleFor(x => x.Answer)
            .NotEmpty().WithMessage("Answer is required.")
            .MaximumLength(SecurityQuestionEntity.Constraints.AnswerMaxLength);
    }
}