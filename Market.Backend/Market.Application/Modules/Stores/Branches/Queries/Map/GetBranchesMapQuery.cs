namespace Market.Application.Modules.Stores.Branches.Queries.Map;

/// Returns all active branches that have coordinates set,
/// together with the parent store name used by the map component.
public sealed class GetBranchesMapQuery : IRequest<List<BranchMapItemDto>>
{
}