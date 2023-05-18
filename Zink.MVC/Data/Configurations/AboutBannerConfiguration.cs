using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zink.MVC.Models;

namespace Zink.MVC.Data.Configurations
{
    public class AboutBannerConfiguration : IEntityTypeConfiguration<AboutBanner>
    {
        public void Configure(EntityTypeBuilder<AboutBanner> modelBuilder)
        {
            modelBuilder.Property(x => x.Title).IsRequired();
            modelBuilder.Property(x => x.Description).IsRequired();
            modelBuilder.Property(x => x.Image).IsRequired();
        }
    }
}
