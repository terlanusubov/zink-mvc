using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zink.MVC.Models;

namespace Zink.MVC.Data.Configurations
{
    public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> modelBuilder)
        {
            modelBuilder.Property(x => x.Name).IsRequired();
            modelBuilder.Property(x => x.ProductId).IsRequired();
        }
    }
}
