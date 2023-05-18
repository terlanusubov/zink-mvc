using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zink.MVC.Models;

namespace Zink.MVC.Data.Configurations
{
    public class HomeBannerConfiguration : IEntityTypeConfiguration<HomeBanner>
    {
        public void Configure(EntityTypeBuilder<HomeBanner> modelBuilder)
        {
            modelBuilder.Property(x => x.Title).IsRequired();
            modelBuilder.Property(x => x.Description).IsRequired();
            modelBuilder.Property(x => x.Image).IsRequired();
        }
    }
}
