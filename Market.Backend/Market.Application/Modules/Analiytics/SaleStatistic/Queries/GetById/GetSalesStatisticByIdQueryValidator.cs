namespace Market.Application.Modules.Analiytics.SaleStatistic.Queries.GetById
{
    public sealed class GetSalesStatisticByIdQueryValidator : AbstractValidator<GetSalesStatisticByIdQuery>
    {
        public GetSalesStatisticByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id must be a positive value.");
        }
    }
}