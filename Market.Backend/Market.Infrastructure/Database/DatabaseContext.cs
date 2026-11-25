using Market.Application.Abstractions;
using Market.Domain.Entities.NotificationEntities;
using Market.Domain.Entities.Analytics;
using Market.Domain.Entities.ProductEntities;
using Market.Domain.Entities.StoreEntities;

namespace Market.Infrastructure.Database;

public partial class DatabaseContext : DbContext, IAppDbContext
{
    public DbSet<ItemComparisonEntity> ItemComparisons { get; set; } = default!;
    public DbSet<ActivityEntity> Activities { get; set; } = default!;
    public DbSet<ManagerEntity> Managers { get; set; } = default!;
    public DbSet<PublicUserEntity> PublicUsers { get; set; } = default!;
    public DbSet<SecurityQuestionEntity> SecurityQuestions { get; set; } = default!;

    public DbSet<MarketUserEntity> Users => Set<MarketUserEntity>();
    public DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();

    public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
    public DbSet<BrandEntity> Brands => Set<BrandEntity>();
    public DbSet<ComparisonEntity> Comparisons => Set<ComparisonEntity>();
    public DbSet<FavoritesEntity> Favorites => Set<FavoritesEntity>();
    public DbSet<ItemComparisonEntity> ItemComparasions => Set<ItemComparisonEntity>();
    public DbSet<PriceEntity> Prices => Set<PriceEntity>();
    public DbSet<ProductEntity> Products => Set<ProductEntity>();
    public DbSet<ReviewEntity> Reviews => Set<ReviewEntity>();
    public DbSet<CityEntity> Cities => Set<CityEntity>();
    public DbSet<StoreEntity> Stores => Set<StoreEntity>();
    public DbSet<BranchEntity> Branches => Set<BranchEntity>();

    public DbSet<NotificationEntity> Notifications =>Set<NotificationEntity>();

    public DbSet<NotificationTypeEntity> NotificationTypes => Set<NotificationTypeEntity>();

    public DbSet<SalesStatisticEntity> SalesStatistics { get; set; } = default!;
    public DbSet<ReportEntity> Report { get; set; } = default!;
    public DbSet<SalesStatisticEntity> SaleStatistics => Set<SalesStatisticEntity>();
    public DbSet<ReportEntity> Reports => Set<ReportEntity>();
    private readonly TimeProvider _clock;

    public DatabaseContext(DbContextOptions<DatabaseContext> options, TimeProvider clock) : base(options)
    {
        _clock = clock;
    }
}