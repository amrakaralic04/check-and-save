namespace Market.Application.Modules.Identity.SecurityQuestion.Command.Update;

public sealed class UpdateSecQCommandValidator
    : AbstractValidator<UpdateSecQCommand>
{
    public UpdateSecQCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Question)
            .NotEmpty()
            .MaximumLength(SecurityQuestionEntity.Constraints.QuestionMaxLength);

        RuleFor(x => x.Answer)
            .NotEmpty()
            .MaximumLength(SecurityQuestionEntity.Constraints.AnswerMaxLength);
    }
}