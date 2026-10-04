using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistences.Configurations
{
    public class ResultOptionEFConfig : IEntityTypeConfiguration<ResultOption>
    {
        public void Configure(EntityTypeBuilder<ResultOption> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.IsCorrect)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasOne(x => x.Question)
                .WithMany(x => x.ResultOptions)
                .HasForeignKey(x => x.QuestionId)
                .IsRequired();
        }
    }
}
