// MarketUserEntity.cs
using Market.Domain.Common;
using Market.Domain.Entities.NotificationEntities;
using Market.Domain.Entities.Analytics;

namespace Market.Domain.Entities.Identity;

public sealed class MarketUserEntity : BaseEntity
{
    public string PublicId { get; set; } = Guid.NewGuid().ToString("N");
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsManager { get; set; }
    public bool IsPublicUser { get; set; }
    public int TokenVersion { get; set; } = 0;
    public bool IsEnabled { get; set; }
    public ICollection<RefreshTokenEntity> RefreshTokens { get; private set; } = new List<RefreshTokenEntity>();
    public ICollection<NotificationEntity> Notifications { get; private set; } = new List<NotificationEntity>();
    public ICollection<ReportEntity> Reports { get; set; } = new List<ReportEntity>();

    public PublicUserEntity? PublicUserEntity { get; set; }
    public ManagerEntity? ManagerEntity { get; set; }
    public SecurityQuestionEntity? SecurityQuestion { get; set; }

    public static class Constraints
    {
        public const int PublicIdMaxLength = 64;
        public const int FirstnameMaxLength = 100;
        public const int LastnameMaxLength = 100;
        public const int EmailMaxLength = 320;
    }
}