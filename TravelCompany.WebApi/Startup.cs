using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using TravelCompany.Core.Services;
using TravelCompany.Core.Services.Implementations;
using TravelCompany.DBLayer.MSSQL;
using TravelCompany.DBLayer.PostgreSQL;
using TravelCompany.Repository;

namespace TravelCompany.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(x => x.AddConsole());

            // Refer to this article if you require more information on CORS
            // https://docs.microsoft.com/en-us/aspnet/core/security/cors
            static void build(CorsPolicyBuilder b) { b.WithOrigins("*").WithMethods("*").WithHeaders("*").Build(); };
            services.AddCors(options => { options.AddPolicy("AllowAllPolicy", build); });

            services.AddMvc(options =>
            {
                // Refer to this article for more details on how to properly set the caching for your needs
                // https://docs.microsoft.com/en-us/aspnet/core/performance/caching/response
                options.CacheProfiles.Add(
                    "default",
                    new CacheProfile
                    {
                        Duration = 600,
                        Location = ResponseCacheLocation.None
                    });
            });

            services.AddResponseCaching(options =>
            {
                options.MaximumBodySize = 2048;
                options.UseCaseSensitivePaths = false;
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "TravelCompany API", Version = "v1" });                
            });
            
            RegisterDBContext(services);
           
            RegisterServices(services);

            services.AddControllers();
        }

        private void RegisterDBContext(IServiceCollection services)
        {
            var dbName = Configuration.GetValue<string>("UseDatabase");
            var dbConnections = Configuration.GetSection("DatabaseConfiguration");

            switch (dbName)
            {
                case "PostgreSQL": {
                        services.AddDbContext<DbContext, PostgreSQLDbContext>(options =>
                        {
                            options.UseNpgsql(dbConnections.GetValue<string>("PostgreSQL"));
                        });
                    } break;

                case "MSSQL": {
                        services.AddDbContext<DbContext, MSSQLDbContext>(options =>
                        {
                            options.UseSqlServer(dbConnections.GetValue<string>("MSSQL"));
                        });
                    }
                    break;
                default:
                    throw new Exception("Incorrect database name in appsettings => UseDatabase section");
            }
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAgencyService, AgencyService>();
            services.AddTransient<IAgentService, AgentService>();            
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {                                
                scope.ServiceProvider.GetRequiredService<DbContext>().Database.Migrate();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            InitializeDatabase(app);

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "TravelCompany API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
