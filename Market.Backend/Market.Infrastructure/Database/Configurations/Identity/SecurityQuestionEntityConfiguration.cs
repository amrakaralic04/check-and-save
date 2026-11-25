namespace Market.Infrastructure.Database.Configurations.Identity;

public sealed class SecurityQuestionEntityConfiguration
    : IEntityTypeConfiguration<SecurityQuestionEntity>
{
    public void Configure(EntityTypeBuilder<SecurityQuestionEntity> b)
    {
        b.ToTable("SecurityQuestions");

        b.HasKey(x => x.Id);

        b.HasOne(x => x.MarketUserEntity)
            .WithOne(x => x.SecurityQuestion)
            .HasForeignKey<SecurityQuestionEntity>(x => x.MarketUserEntityId)
            .OnDelete(DeleteBehavior.Cascade);

        b.Property(x => x.Question)
            .IsRequired()
            .HasMaxLength(SecurityQuestionEntity.Constraints.QuestionMaxLength);

        b.Property(x => x.Answer)
            .IsRequired()
            .HasMaxLength(SecurityQuestionEntity.Constraints.AnswerMaxLength);
    }
}