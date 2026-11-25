namespace Market.Application.Modules.Analiytics.SaleStatistic.Commands.Create
{
    public sealed class CreateSalesStatisticCommandValidator : AbstractValidator<CreateSalesStatisticCommand>
    {
        public CreateSalesStatisticCommandValidator()
        {
            RuleFor(x => x.ManagerEntityId)
                .GreaterThan(0);

            RuleFor(x => x.ProductEntityId)
                .GreaterThan(0);

            RuleFor(x => x.ViewsCount)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.SalesCount)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Date)
                .NotEmpty();
        }
    }
}