namespace Market.Application.Modules.Analiytics.Report.Queries.GetById
{
    public sealed class GetReportByIdQueryValidator : AbstractValidator<GetReportByIdQuery>
    {
        public GetReportByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id must be a positive value.");
        }
    }
}