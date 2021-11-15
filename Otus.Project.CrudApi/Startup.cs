using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Otus.Project.CrudApi.Extensions;
using Otus.Project.CrudApi.Services;
using Otus.Project.Orm.Configuration;
using Otus.Project.Orm.Repository;
using Prometheus;
using System;

namespace Otus.Project.CrudApi
{
    public class Startup
    {
        private const string Readiness = "Readiness";
        private const string HealthCheckSql = "SELECT 1 FROM User;";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Environment.GetEnvironmentVariable("DATABASE_URI")
                ?? Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<StorageContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseNpgsql(connection);
            });

            services.AddControllers();
            services.AddHealthChecks()
                .AddNpgSql(connection, healthQuery: HealthCheckSql, tags: new string[] { Readiness });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Otus.Project.CrudApi", Version = "v1" });
            });

            services.AddScoped<DbContext, StorageContext>();
            services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IHostApplicationLifetime hostApplicationLifetime)
        {
            // # To peform database migration:
            // dotnet Otus.Project.CrudApi.dll --migrate=true
            hostApplicationLifetime.AddDbMigrationHandler(Configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Otus.Project.CrudApi v1"));
            }

            app.UseRouting();
            app.UseHttpMetrics();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {                
                endpoints.MapControllers();
                endpoints.MapMetrics();

                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = check => !check.Tags.Contains(Readiness)
                });

                endpoints.MapHealthChecks("/readiness", new HealthCheckOptions
                {
                    Predicate = check => check.Tags.Contains(Readiness)
                });
            });
        }
    }
}
