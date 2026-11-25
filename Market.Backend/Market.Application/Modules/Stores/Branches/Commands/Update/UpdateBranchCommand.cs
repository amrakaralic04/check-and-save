using System.Text.Json.Serialization;
using MediatR;

namespace Market.Application.Modules.Stores.Branches.Commands.Update;

public sealed class UpdateBranchCommand : IRequest<Unit>
{
    [JsonIgnore]
    public int Id { get; set; }

    public int StoreEntityId { get; set; }
    public int CityEntityId { get; set; }

    public string Address { get; set; }
    public string Contact { get; set; }
    public string Email { get; set; }

    public bool IsActive { get; set; }
}