using Market.Domain.Entities.Analytics;

namespace Market.Application.Modules.Analiytics.Report.Commands.Update
{
    public sealed class UpdateReportCommandValidator : AbstractValidator<UpdateReportCommand>
    {
        public UpdateReportCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

            RuleFor(x => x.ReportType)
                .NotEmpty().WithMessage("ReportType is required.")
                .MaximumLength(ReportEntity.Constraints.ReportTypeMaxLength);

            RuleFor(x => x.Description)
                .MaximumLength(ReportEntity.Constraints.DescriptionMaxLength)
                .When(x => x.Description is not null);

            RuleFor(x => x.ReportDate)
                .NotEmpty();
        }
    }
}