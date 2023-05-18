using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zink.MVC.Models;

namespace Zink.MVC.Data.Configurations
{
    public class KeyFeatureConfiguration : IEntityTypeConfiguration<KeyFeature>
    {
        public void Configure(EntityTypeBuilder<KeyFeature> modelBuilder)
        {
            modelBuilder.Property(x => x.Name).IsRequired();
            modelBuilder.Property(x => x.ProductId).IsRequired();
        }
    }
}
