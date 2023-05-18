using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zink.MVC.Models;

namespace Zink.MVC.Data.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> modelBuilder)
        {
            modelBuilder.Property(x => x.Email).IsRequired();
            modelBuilder.Property(x => x.PhoneNumber).IsRequired();
            modelBuilder.Property(x => x.Address).IsRequired();
        }
    }
}
