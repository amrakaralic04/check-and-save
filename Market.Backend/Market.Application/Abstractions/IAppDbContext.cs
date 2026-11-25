using Market.Domain.Entities.NotificationEntities;
﻿using Market.Domain.Entities.Analytics;
using Market.Domain.Entities.ProductEntities;
using Market.Domain.Entities.StoreEntities;

namespace Market.Application.Abstractions;

public interface IAppDbContext
{
    DbSet<MarketUserEntity> Users { get; }

    DbSet<RefreshTokenEntity> RefreshTokens { get; }
    DbSet<ComparisonEntity> Comparisons { get; }
    DbSet<FavoritesEntity> Favorites { get; }
    DbSet<ItemComparisonEntity> ItemComparisons { get; }
    DbSet<PriceEntity> Prices { get; }
    DbSet<ProductEntity> Products { get; }
    DbSet<ReviewEntity> Reviews { get; }
    DbSet<BrandEntity> Brands { get; }
    DbSet<CategoryEntity> Categories { get; }
    DbSet<ActivityEntity> Activities { get; }
    DbSet<ManagerEntity> Managers { get; }
    DbSet<PublicUserEntity> PublicUsers { get; }
    DbSet<SecurityQuestionEntity> SecurityQuestions { get; }
    DbSet<NotificationEntity> Notifications { get; }
    DbSet<NotificationTypeEntity> NotificationTypes { get; }

    DbSet<CityEntity> Cities { get; }
    DbSet<BranchEntity> Branches { get; }
    DbSet<StoreEntity> Stores { get; }
    DbSet<ReportEntity> Reports { get; }
    DbSet<SalesStatisticEntity> SaleStatistics { get; }

    Task<int> SaveChangesAsync(CancellationToken ct);
}