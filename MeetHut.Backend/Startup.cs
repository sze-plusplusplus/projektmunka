using System.Text;
using AutoMapper;
using MeetHut.Backend.Configuration;
using MeetHut.Backend.Middlewares;
using MeetHut.DataAccess;
using MeetHut.Services.Application;
using MeetHut.Services.Application.Mappers;
using MeetHut.Services.Meet;
using MeetHut.Services.Meet.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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
            services.Configure<ApplicationConfiguration>(Configuration);
            services.Configure<MigrationConfiguration>(Configuration.GetSection("Migration"));


            // Add Database context
            string connectionString = Configuration.GetConnectionString(Configuration.GetValue<bool>("UseDesignTimeConnection") ? "DesignTimeConnection" : "DefaultConnection");
            services.AddDbContextPool<DatabaseContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                    builder => builder.MigrationsAssembly("MeetHut.DataAccess")));


            // Add services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoomService, RoomService>();

            // Add mappers
            var mapperConfig = new MapperConfiguration(conf => { conf.AddProfile<UserMapper>(); conf.AddProfile<RoomMapper>(); });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Add controllers
            services.AddControllers();

            // Add session
            // services.AddSession();

            // Register auth
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            // Register swagger display
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MeetHut.Backend", Version = "v1" });
            });

            services.AddSpaStaticFiles(conf =>
            {
                conf.RootPath = "./ClientApp/dist/frontend/";
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

            app.UseWebSockets();

            // app.UseSession();

            /* app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }

                await next();
            });*/

            app.UseServerExceptionHandler();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "./ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer(Configuration["ClientUrl"]);
                }
            });
        }
    }
}