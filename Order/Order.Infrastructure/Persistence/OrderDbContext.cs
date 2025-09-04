using Core.Base;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Persistence
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("Orders");
                entity.HasKey(e => e.RecID);
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.ToTable("OrderDetails");
                entity.HasKey(e => e.RecID);
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
