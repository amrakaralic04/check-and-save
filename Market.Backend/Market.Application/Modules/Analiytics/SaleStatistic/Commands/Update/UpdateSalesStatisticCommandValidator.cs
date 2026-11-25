namespace Market.Application.Modules.Analiytics.SaleStatistic.Commands.Update
{
    public sealed class UpdateSalesStatisticCommandValidator : AbstractValidator<UpdateSalesStatisticCommand>
    {
        public UpdateSalesStatisticCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.ViewsCount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.SalesCount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Date).NotEmpty();
        }
    }
}