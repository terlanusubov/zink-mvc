using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zink.MVC.Models;

namespace Zink.MVC.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.Property(x => x.Name).IsRequired();
            modelBuilder.Property(x => x.Surname).IsRequired();
            modelBuilder.Property(x => x.Email).IsRequired();
            modelBuilder.Property(x => x.Password).IsRequired();
            modelBuilder.Property(x => x.UserRoleId).IsRequired();
        }
    }
}
