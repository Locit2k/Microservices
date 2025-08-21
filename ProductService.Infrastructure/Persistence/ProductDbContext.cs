
using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Persistence
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Products> Products { get; set; }

        public DbSet<Categories> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(e => e.CategoryID);
                entity.Property(e => e.CategoryName).IsRequired().HasMaxLength(100);
            });
            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(e => e.ProductID);
                entity.Property(e => e.ProductName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Category).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasDefaultValue(string.Empty);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)").HasDefaultValue(0);
                entity.Property(e => e.Stock).HasDefaultValue(0);
                entity.Property(e => e.ImageUrl).HasMaxLength(500);
                entity.Property(e => e.IsActive).HasDefaultValue(false);
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Apply audit fields for CreatedBy, CreatedOn, ModifiedBy, and ModifiedOn.
        /// </summary>
        private void ApplyAuditFields()
        {
            var changeTracker = "Admin";
            var now = DateTime.Now;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = changeTracker;
                    entry.Entity.CreatedOn = now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedBy = changeTracker;
                    entry.Entity.ModifiedOn = now;

                    // Đảm bảo CreatedBy và CreatedOn không bị chỉnh sửa
                    entry.Property(x => x.CreatedBy).IsModified = false;
                    entry.Property(x => x.CreatedOn).IsModified = false;
                }
            }
        }
    }
}
