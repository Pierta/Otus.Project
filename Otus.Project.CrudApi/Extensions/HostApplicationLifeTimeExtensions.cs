using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Otus.Project.Orm.Configuration;
using System;

namespace Otus.Project.CrudApi.Extensions
{
    public static class HostApplicationLifeTimeExtensions
    {
        public static void AddDbMigrationHandler(this IHostApplicationLifetime appLifetime, IConfiguration configuration)
        {
            appLifetime.ApplicationStarted.Register(async () =>
            {
                var migrateArg = configuration["migrate"];
                if (migrateArg == "true")
                {
                    var builder = new DbContextOptionsBuilder<StorageContext>();
                    var connectionString = configuration.GetConnectionString("DefaultConnection");
                    builder.UseNpgsql(connectionString);
                    using (var dbContext = new StorageContext(builder.Options))
                    {
                        await dbContext.Database.MigrateAsync();
                    }
                    appLifetime.StopApplication();
                }
                else if (migrateArg != null)
                {
                    Console.WriteLine("\t\nInvalid value for 'migrate' argument. Valid values: true.\n");
                }
            });
        }
    }
}
