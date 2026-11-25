namespace Market.Application.Modules.Notifications.NotificationType.Queries.GetById
{
    public sealed class GetNotifTypeByIdQryValidator : AbstractValidator<GetNotifTypeByIdQry>
    {
        public GetNotifTypeByIdQryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
