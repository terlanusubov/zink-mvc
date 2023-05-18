using Microsoft.EntityFrameworkCore;
using Zink.MVC.Data.Configurations;
using Zink.MVC.Models;

namespace Zink.MVC.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new AboutConfiguration());
            modelBuilder.ApplyConfiguration(new AboutBannerConfiguration());
            modelBuilder.ApplyConfiguration(new HomeBannerConfiguration());
            modelBuilder.ApplyConfiguration(new CertificateConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new KeyFeatureConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SocialMediaConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<WhoWeAre> Abouts { get; set; }
        public virtual DbSet<AboutBanner> AboutBanners { get; set; }
        public virtual DbSet<HomeBanner> HomeBanners { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<KeyFeature> KeyFeatures { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<SocialMedia> SocialMedias { get; set; }
    }
}
