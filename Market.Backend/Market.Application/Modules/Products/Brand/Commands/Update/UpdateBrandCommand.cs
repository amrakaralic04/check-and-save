namespace Market.Application.Modules.Products.Brand.Commands.Update
{
    public sealed class UpdateBrandCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
