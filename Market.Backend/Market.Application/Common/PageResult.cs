using Market.Application.Modules.Identity.Activity.Queries.List;
using Market.Application.Modules.Identity.Manager.Queries.List;
using Market.Application.Modules.Identity.User.Queries.List;
using Market.Application.Modules.Products.Comparison.Queries.List;
using Market.Application.Modules.Products.Favorites.Queries.List;
using Market.Application.Modules.Products.ItemComparison.Queries.List;
using Market.Application.Modules.Products.Price.Queries.List;
using Market.Application.Modules.Products.Product.Queries.List;
using Market.Application.Modules.Products.Review.Queries.List;

namespace Market.Application.Common;

public sealed class PageResult<T>
{
    public required IReadOnlyList<T> Items { get; init; }
    public required int PageSize { get; init; }
    public required int CurrentPage { get; init; }
    public required bool IncludedTotal { get; init; }
    public required int TotalItems { get; init; }
    public required int TotalPages { get; init; }

    /// <summary>
    /// Creates a PageResult from an IQueryable using EF Core asynchronous methods.
    /// </summary>
    public static async Task<PageResult<T>> FromQueryableAsync(
        IQueryable<T> query,
        PageRequest paging,
        CancellationToken ct = default,
        bool includeTotal = true)
    {
        int total = 0;
        if (includeTotal)
            total = await query.CountAsync(ct);

        var items = await query
            .Skip(paging.SkipCount)
            .Take(paging.PageSize)
            .ToListAsync(ct);

        return new PageResult<T> {
            Items = items,
            PageSize = paging.PageSize,
            CurrentPage = paging.Page,
            IncludedTotal = includeTotal,
            TotalItems = total,
            TotalPages = includeTotal ? (int)Math.Ceiling(total / (double)paging.PageSize) : 0
        };
    }

    internal static async Task<PageResult<ListComparisonsQueryDto>> FromQueryableAsync(IQueryable<ListComparisonsQueryDto> pq, object page, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    internal static async Task<PageResult<ListFavoritesQueryDto>> FromQueryableAsync(IQueryable<ListFavoritesQueryDto> pq, object page, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    internal static async Task<PageResult<ListItemComparisonsQueryDto>> FromQueryableAsync(IQueryable<ListItemComparisonsQueryDto> pq, object page, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    internal static async Task<PageResult<ListPricesQueryDto>> FromQueryableAsync(IQueryable<ListPricesQueryDto> pq, object page, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    internal static async Task<PageResult<ListProductsQueryDto>> FromQueryableAsync(IQueryable<ListProductsQueryDto> pq, object page, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    internal static async Task<PageResult<ListReviewsQueryDto>> FromQueryableAsync(IQueryable<ListReviewsQueryDto> pq, object page, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    internal static async Task<PageResult<ListActivitiesQueryDto>> FromQueryableAsync(IQueryable<ListActivitiesQueryDto> pq, object page, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    internal static async Task<PageResult<ListManagersQueryDto>> FromQueryableAsync(IQueryable<ListManagersQueryDto> pq, object page, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    internal static async Task<PageResult<ListMarketUsersQueryDto>> FromQueryableAsync(IQueryable<ListMarketUsersQueryDto> pq, object page, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}