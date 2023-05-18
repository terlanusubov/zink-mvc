using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zink.MVC.Models;

namespace Zink.MVC.Data.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> modelBuilder)
        {
            modelBuilder.Property(x => x.Name).IsRequired();
            modelBuilder.Property(x => x.Surname).IsRequired();
            modelBuilder.Property(x => x.Email).IsRequired();
            modelBuilder.Property(x => x.PhoneNumber).IsRequired();
            modelBuilder.Property(x => x.Message).HasMaxLength(500).IsRequired();
        }
    }
}
