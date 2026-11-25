using Market.Domain.Common;

namespace Market.Domain.Entities.Identity
{
    public class SecurityQuestionEntity : BaseEntity
    {
        public int MarketUserEntityId { get; set; }
        public MarketUserEntity? MarketUserEntity { get; set; }
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;

        public static class Constraints
        {
            public const int QuestionMaxLength = 200;
            public const int AnswerMaxLength = 200;
        }
    }
}