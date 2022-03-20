using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using EasyNetQ.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Otus.Project.MessageBus.Contracts;
using Otus.Project.OrderApi.Extensions;
using Otus.Project.OrderApi.Services;
using Otus.Project.OrderApi.Settings;
using Otus.Project.Orm.Configuration;
using Otus.Project.Orm.Repository;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Otus.Project.OrderApi
{
    public class Startup
    {
        private const string Readiness = "Readiness";
        private const string HealthCheckSql = "SELECT 1 FROM User;";

        private const string SubscriptionIdPrefix = "OrderService";

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                {
                    var options = Configuration.GetSection("AppSettings");
                    cfg.RequireHttpsMetadata = false;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options["Secret"]))
                    };
                });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers();
            services.AddHealthChecks()
                .AddNpgSql(connection, healthQuery: HealthCheckSql, tags: new string[] { Readiness });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Otus.Project.OrderApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT (with Bearer prefix)",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<ExternalServices>((o) => o.BillingApi = new BillingApi
            {
                Url = Environment.GetEnvironmentVariable("BILLINGAPI_URI")
                    ?? Configuration["ExternalServices:BillingApi:Url"]
            });

            services.AddScoped<DbContext, StorageContext>();
            services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ISagaOrderService, SagaOrderService>();
            services.AddScoped<IBillingApiClient, BillingApiClient>();

            // service bus infrastructure configuration
            string serviceBusConnection = Environment.GetEnvironmentVariable("SERVICEBUS_CONNECTION")
                ?? Configuration["ServiceBusSettings:Connection"];

            services.AddSingleton(RabbitHutch.CreateBus(serviceBusConnection));
            services.AddSingleton<MessageDispatcher>();
            services.AddSingleton(provider =>
            {
                return new AutoSubscriber(provider.GetRequiredService<IBus>(), SubscriptionIdPrefix)
                {
                    AutoSubscriberMessageDispatcher = provider.GetRequiredService<MessageDispatcher>()
                };
            });

            // message handlers registration
            services.AddScoped<PaymentCompletedConsumer>();
            services.AddScoped<NoStockConsumer>();
            services.AddScoped<StockReleasedConsumer>();

            // configure console logging for EasyNetQ
            LogProvider.SetCurrentLogProvider(ConsoleLogProvider.Instance);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Otus.Project.OrderApi v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ContextMiddleware>();

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

            app.ApplicationServices.GetRequiredService<AutoSubscriber>().SubscribeAsync(new[] { Assembly.GetExecutingAssembly() });
        }
    }
}
