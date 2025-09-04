
using Core.Base;
using Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Infrastructure.Persistence
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }
        public DbSet<Customers> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.ToTable("Customers");
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
