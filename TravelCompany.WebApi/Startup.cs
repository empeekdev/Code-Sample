using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TravelCompany.Core.Services;
using TravelCompany.Core.Services.Implementations;
using TravelCompany.DBLayer.MSSQL;
using TravelCompany.DBLayer.PostgreSQL;
using Newtonsoft.Json.Serialization;
using Microsoft.OpenApi.Models;

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

            //services.AddDbContext<DbContext, PostgreSQLDbContext>(options => {
            //    options.UseNpgsql(Configuration.GetSection("DatabaseConfiguration").GetValue<string>("PostgreSQL"));
            //});

            services.AddDbContext<DbContext, MSSQLDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetSection("DatabaseConfiguration").GetValue<string>("MSSQL"));
            });

            services.AddTransient<ITravelAgencyService, TravelAgencyService >();

            services.AddControllers();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                //scope.ServiceProvider.GetRequiredService<DbContext>().Database.Migrate();
                scope.ServiceProvider.GetRequiredService<DbContext>().Database.EnsureCreated();
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
