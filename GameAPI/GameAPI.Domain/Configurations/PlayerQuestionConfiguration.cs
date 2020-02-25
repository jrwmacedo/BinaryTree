using GameAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameAPI.Domain.Configurations
{
    public class PlayerQuestionConfiguration : IEntityTypeConfiguration<PlayerQuestion>
    {
        public void Configure(EntityTypeBuilder<PlayerQuestion> builder)
        {
            builder.HasKey(pq => new { pq.PlayerId, pq.QuestionId });
            builder.Property(f => f.PlayerId).ValueGeneratedNever();
            builder.Property(f => f.QuestionId).ValueGeneratedNever();

            builder.HasOne<Player>(pq => pq.Player)
                .WithMany(p => p.PlayerQuestions)
                .HasForeignKey(sc => sc.PlayerId);

            builder
                .HasOne<Question>(pq => pq.Question)
                .WithMany(p => p.PlayerQuestions)
                .HasForeignKey(pq => pq.QuestionId);
        }
    }
}
