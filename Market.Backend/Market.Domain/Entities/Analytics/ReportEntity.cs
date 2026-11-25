using Market.Domain.Common;
using Market.Domain.Entities.Identity;

namespace Market.Domain.Entities.Analytics
{
    public class ReportEntity : BaseEntity
    {
        public int MarketUserEntityId { get; set; }

        public string ReportType { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime ReportDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }

        public MarketUserEntity MarketUserEntity { get; set; } = default!;

        public static class Constraints
        {
            public const int ReportTypeMaxLength = 100;
            public const int DescriptionMaxLength = 2000;
        }
    }
}