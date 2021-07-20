using MeetHut.Backend.Middlewares;
using MeetHut.DataAccess;
using MeetHut.Services.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace MeetHut.Backend
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        /// <summary>
        /// Application configuration
        /// </summary>
        private IConfiguration Configuration { get; }

        
        /// <summary>
        /// Configure services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Database context
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<DatabaseContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                    builder => builder.MigrationsAssembly("MeetHut.Backend")));
            
            // Add services
            services.AddScoped<IUserService, UserService>();
            
            // Add controllers
            services.AddControllers();
            
            // Register swagger display
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "MeetHut.Backend", Version = "v1"});
            });
        }
        
        /// <summary>
        /// Configure application builder during startup
        /// </summary>
        /// <param name="app">App builder</param>
        /// <param name="env">Environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MeetHut.Backend v1"));
            }

            app.UseServerExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}