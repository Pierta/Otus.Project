using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
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
using Prometheus.HttpMetrics;
using System;
using System.Diagnostics;
using System.Linq;

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

            var histogram = Metrics.CreateHistogram("crud_api_http_request_duration_seconds", "Crud-api requests latency",
                new HistogramConfiguration
                {
                    Buckets = Histogram.ExponentialBuckets(0.001, 2, 16),
                    LabelNames = HttpRequestLabelNames.All.Union(new[] { "path" }).ToArray()
                });
            var counter = Metrics.CreateCounter("crud_api_http_requests_received_total", "Crud-api requests count",
                new CounterConfiguration
                {
                    LabelNames = HttpRequestLabelNames.All.Union(new[] { "path" }).ToArray()
                });

            app.Use((context, next) =>
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                // We need to write this out in long form instead of using a timer because routing data in
                // ASP.NET Core 2 is only available *after* executing the next request delegate.
                // So we would not have the right labels if we tried to create the child early on.
                try
                {
                    return next();
                }
                finally
                {
                    var labelValues = new string[]
                    {
                        context.Response.StatusCode.ToString(),
                        context.Request.Method,
                        context.Request.RouteValues["controller"]?.ToString() ?? string.Empty,
                        context.Request.RouteValues["action"]?.ToString() ?? string.Empty,
                        context.Request.Path
                    };
                    
                    histogram.WithLabels(labelValues).Observe(stopWatch.Elapsed.TotalSeconds);
                    counter.WithLabels(labelValues).Inc();
                }
            });

            app.UseHttpMetrics(options =>
            {
                options.AddRouteParameter("path");
                options.RequestDuration.Histogram = histogram; // replace default histogram with the custom one
                options.RequestCount.Counter = counter; // replace default counter with the custom one
            });

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
