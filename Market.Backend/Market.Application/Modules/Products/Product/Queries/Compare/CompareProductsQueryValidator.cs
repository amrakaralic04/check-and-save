namespace Market.Application.Modules.Products.Product.Queries.Compare
{
    public sealed class CompareProductsQueryValidator : AbstractValidator<CompareProductsQuery>
    {
        public CompareProductsQueryValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty()
                .WithMessage("Potrebno je poslati barem jedan publicId kroz ids query parametar.")
                .Must(BeValidPublicIds)
                .WithMessage("Ids mora sadržavati pozitivne numeričke publicId vrijednosti odvojene zarezom.");
        }

        private static bool BeValidPublicIds(string? ids)
        {
            if (string.IsNullOrWhiteSpace(ids))
                return false;

            return ids.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .All(id => int.TryParse(id, out var value) && value > 0);
        }
    }
}