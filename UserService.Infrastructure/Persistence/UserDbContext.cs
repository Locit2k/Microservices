using Microsoft.EntityFrameworkCore;
using System.Data;
using UserService.Domain.Entities;
namespace UserService.Infrastructure.Persistence
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Users>(entities =>
            {
                entities.ToTable("Users");
                entities.HasKey(e => e.UserID);
                entities.Property(e => e.UserID).HasMaxLength(50);
                entities.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entities.Property(e => e.Gender).IsRequired();
                entities.Property(e => e.Birthday).IsRequired();
                entities.Property(e => e.Phone).IsRequired().HasMaxLength(20);
                entities.Property(e => e.Address).IsRequired(false);
                entities.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entities.Property(e => e.CreatedBy).IsRequired().HasMaxLength(50);
                entities.Property(e => e.CreatedOn).HasDefaultValueSql("GETDATE()");
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
