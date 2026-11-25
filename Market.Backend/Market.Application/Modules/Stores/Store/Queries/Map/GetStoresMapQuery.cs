namespace Market.Application.Modules.Store.Stores.Queries.Map;

/// <summary>
/// Returns all active stores with coordinates for map rendering.
/// Accessible to authenticated users (admin, manager, public user).
/// </summary>
public sealed class GetStoresMapQuery : IRequest<List<StoreMapItemDto>>
{
}