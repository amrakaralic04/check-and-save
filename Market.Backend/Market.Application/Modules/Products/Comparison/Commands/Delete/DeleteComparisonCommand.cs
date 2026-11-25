namespace Market.Application.Modules.Products.Comparison.Commands.Delete
{
    public sealed class DeleteComparisonCommand : IRequest<Unit>
    {

        [JsonIgnore]
        public int Id { get; set; }
    }
}
