namespace Market.Application.Modules.Stores.City.Commands.Create
{
    public sealed class CreateCityCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int PostalCode { get; set; }
    }
}