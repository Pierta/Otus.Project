using Microsoft.EntityFrameworkCore;
using Otus.Project.Domain.Model;

namespace Otus.Project.Orm.Configuration
{
    public class StorageContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public StorageContext()
        {
        }

        public StorageContext(DbContextOptions<StorageContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
