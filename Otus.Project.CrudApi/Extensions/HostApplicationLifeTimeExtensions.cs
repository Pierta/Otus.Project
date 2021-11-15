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
                    Console.WriteLine("Starting migration...");
                    var builder = new DbContextOptionsBuilder<StorageContext>();
                    string connection = Environment.GetEnvironmentVariable("DATABASE_URI")
                        ?? configuration.GetConnectionString("DefaultConnection");
                    builder.UseNpgsql(connection);
                    Console.WriteLine(connection);
                    using (var dbContext = new StorageContext(builder.Options))
                    {
                        await dbContext.Database.MigrateAsync();
                    }
                    Console.WriteLine("Migration completed!");
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
