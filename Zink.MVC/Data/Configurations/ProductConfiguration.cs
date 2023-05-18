using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zink.MVC.Models;

namespace Zink.MVC.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> modelBuilder)
        {
            modelBuilder.Property(x => x.Name).IsRequired();
            modelBuilder.Property(x => x.Description).IsRequired();
            modelBuilder.Property(x => x.Image).IsRequired();
            modelBuilder.Property(x => x.Price).HasColumnType("decimal(18,2)").IsRequired();
            modelBuilder.Property(x => x.ProductStatusId).IsRequired();
            modelBuilder.Property(x => x.CategoryId).IsRequired();
            modelBuilder.Ignore(x => x.ImageFile);

        }
    }
}
