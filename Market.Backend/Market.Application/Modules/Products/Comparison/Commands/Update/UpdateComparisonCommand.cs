namespace Market.Application.Modules.Products.Comparison.Commands.Update
{
    public sealed class UpdateComparisonCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public DateTime Date { get; set; }
    }
}
