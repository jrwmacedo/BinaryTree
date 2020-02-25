using GameAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameAPI.Domain.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(f => f.Id).ValueGeneratedNever();

            builder.Property(p => p.Description)
                .IsRequired();

            builder.Property(p => p.ParentId)
               .IsRequired(false);

            builder
            .HasMany(q => q.Children)
            .WithOne(q => q.Parent)
            .HasForeignKey(q => q.ParentId);

            builder.HasData(new Question { Id= 1, Description = "Are you going to celebrate carnival in your country ?" });
            builder.HasData(new Question { Id= 2, Description = "Are you going to wear a fancy-dress costume ?", YesNo= true, ParentId = 1 });
            builder.HasData(new Question { Id = 3, Description = "Are you going to visit somewhere ?", YesNo = false, ParentId = 1 });
            builder.HasData(new Question { Id = 4, Description = "Cool!", YesNo = true, ParentId = 2 });
            builder.HasData(new Question { Id = 5, Description = "Enjoy the party anyway!", YesNo = false, ParentId = 2 });
            builder.HasData(new Question { Id = 6, Description = "Enjoy it!", YesNo = true, ParentId = 3 });
            builder.HasData(new Question { Id = 7, Description = "So, take some rest.", YesNo = false, ParentId = 3 });
        }
    }
}
