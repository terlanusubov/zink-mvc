using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zink.MVC.Models;

namespace Zink.MVC.Data.Configurations
{
    public class SocialMediaConfiguration : IEntityTypeConfiguration<SocialMedia>
    {
        public void Configure(EntityTypeBuilder<SocialMedia> modelBuilder)
        {
            modelBuilder.Property(x => x.Url).IsRequired();
            modelBuilder.Property(x => x.Name).IsRequired();
            modelBuilder.Property(x => x.Icon).IsRequired();
        }
    }
}
