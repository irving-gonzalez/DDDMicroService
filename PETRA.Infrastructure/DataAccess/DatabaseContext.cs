using Microsoft.EntityFrameworkCore;
using PETRA.Domain.AggregatesModel;
using PETRA.Domain.Entities;
using PETRA.Infrastructure.DataAccess.EntityConfig;

namespace PETRA.Infrastructure.DataAccess
{
      public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Seed();

            //Configure Domain entities
            modelBuilder.ApplyConfiguration(new UserConfig());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddBaseEntityFields();
            return base.SaveChangesAsync();
        }

        public void AddBaseEntityFields()
        {
            foreach (var baseEntity in ChangeTracker.Entries<BaseEntity>())
            {
                switch (baseEntity.State)
                {
                    case EntityState.Added:
                        baseEntity.Entity.CreatedDateUtc = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        baseEntity.Property(p => p.CreatedDateUtc).IsModified = false;
                        baseEntity.Entity.LastUpdatedDateUtc = DateTime.UtcNow;
                        break;
                }
            }
        }

        public DbSet<User> User { get; set; }
    }
}