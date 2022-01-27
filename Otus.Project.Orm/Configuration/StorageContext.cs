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
            modelBuilder.ApplyConfiguration(new BillingAccountMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new OrderProductsMap());
            modelBuilder.ApplyConfiguration(new NotificationMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
