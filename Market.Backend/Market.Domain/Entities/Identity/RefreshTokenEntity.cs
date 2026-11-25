using Market.Domain.Common;

namespace Market.Domain.Entities.Identity;

public sealed class RefreshTokenEntity : BaseEntity
{
    public string? TokenHash { get; set; }
    public DateTime ExpiresAtUtc { get; set; }
    public bool IsRevoked { get; set; }
    public int UserId { get; set; }
    public MarketUserEntity User { get; set; } = default!;
    public string? Fingerprint { get; set; }
    public DateTime? RevokedAtUtc { get; set; }

    public static class Constraints
    {
        public const int TokenHashMaxLength = 200;
        public const int FingerprintMaxLength = 200;
    }
}