using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zink.MVC.Models;

namespace Zink.MVC.Data.Configurations
{
    public class AboutConfiguration : IEntityTypeConfiguration<WhoWeAre>
    {
        public void Configure(EntityTypeBuilder<WhoWeAre> modelBuilder)
        {
            modelBuilder.Property(x => x.Title).IsRequired();
            modelBuilder.Property(x => x.Digit).IsRequired();
            modelBuilder.Property(x => x.Image).IsRequired();
        }
    }
}
