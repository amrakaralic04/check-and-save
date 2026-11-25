namespace Market.Application.Modules.Analiytics.Report.Commands.Delete
{
    public sealed class DeleteReportCommandHandler(IAppDbContext ctx)
    : IRequestHandler<DeleteReportCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteReportCommand request, CancellationToken ct)
        {
            var entity = await ctx.Reports
                .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, ct);

            if (entity is null)
                throw new MarketNotFoundException($"Report (ID={request.Id}) not found.");

            entity.IsDeleted = true;
            entity.ModifiedAt = DateTime.UtcNow;

            await ctx.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}