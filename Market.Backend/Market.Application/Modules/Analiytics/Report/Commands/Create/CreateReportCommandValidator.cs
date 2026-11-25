using Market.Domain.Entities.Analytics;

namespace Market.Application.Modules.Analiytics.Report.Commands.Create
{
    public sealed class CreateReportCommandValidator : AbstractValidator<CreateReportCommand>
    {
        public CreateReportCommandValidator()
        {
            RuleFor(x => x.MarketUserEntityId)
                .GreaterThan(0).WithMessage("MarketUserEntityId must be greater than zero.");

            RuleFor(x => x.ReportType)
                .NotEmpty().WithMessage("ReportType is required.")
                .MaximumLength(ReportEntity.Constraints.ReportTypeMaxLength)
                .WithMessage($"ReportType can be at most {ReportEntity.Constraints.ReportTypeMaxLength} characters long.");

            RuleFor(x => x.Description)
                .MaximumLength(ReportEntity.Constraints.DescriptionMaxLength)
                .WithMessage($"Description can be at most {ReportEntity.Constraints.DescriptionMaxLength} characters long.")
                .When(x => x.Description is not null);

            RuleFor(x => x.ReportDate)
                .NotEmpty().WithMessage("ReportDate is required.");
        }
    }
}