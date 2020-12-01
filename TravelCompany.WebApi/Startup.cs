using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TravelCompany.Core.Services;
using TravelCompany.Core.Services.Implementations;
using TravelCompany.DBLayer.MSSQL;
using TravelCompany.DBLayer.PostgreSQL;

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

            //services.AddDbContext<DbContext, PostgreSQLDbContext>(options => {
            //    options.UseNpgsql(Configuration.GetSection("DatabaseConfiguration").GetValue<string>("PostgreSQL"));
            //});

            services.AddDbContext<DbContext, MSSQLDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetSection("DatabaseConfiguration").GetValue<string>("MSSQL"));
            });

            services.AddTransient<ITestService, TestService>();

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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
