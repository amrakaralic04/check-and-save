namespace Market.Application.Modules.Products.Price.Commands.Update
{
    public sealed class UpdatePriceCommandValidator : AbstractValidator<UpdatePriceCommand>
    {
        public UpdatePriceCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.DateUpdated).NotEmpty();
        }
    }
}
