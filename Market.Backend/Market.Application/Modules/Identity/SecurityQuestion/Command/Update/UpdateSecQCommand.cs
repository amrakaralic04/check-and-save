namespace Market.Application.Modules.Identity.SecurityQuestion.Command.Update;

public sealed class UpdateSecQCommand : IRequest<Unit>
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Question { get; set; }
    public string Answer { get; set; }
}