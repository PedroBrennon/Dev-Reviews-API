using DevReviews.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevReviews.Repository.Persistence
{
    public class DevReviewsDbContext : DbContext
    {
        public DevReviewsDbContext(DbContextOptions<DevReviewsDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(p => {
                p.ToTable("tb_Product");
                p.HasKey(p => p.Id);

                p
                    .HasMany(pp => pp.Reviews)
                    .WithOne()
                    .HasForeignKey(r => r.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductReview>(pr => {
                pr.ToTable("tb_ProductReviews");
                pr.HasKey(p => p.Id);

                pr.Property(p => p.Author)
                    .HasMaxLength(50)
                    .IsRequired();
                //.HasColumnName("author");
            });
        }
    }
}