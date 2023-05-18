using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zink.MVC.Models;

namespace Zink.MVC.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> modelBuilder)
        {
            modelBuilder.Property(x => x.Name).IsRequired();
            modelBuilder.Property(x => x.Description).IsRequired();
            modelBuilder.Property(x => x.Image).IsRequired();
            modelBuilder.Property(x => x.ParentId).IsRequired();
            modelBuilder.Property(x => x.CategoryStatusId).IsRequired();
            modelBuilder.Ignore(x => x.ImageFile);
        }
    }
}
