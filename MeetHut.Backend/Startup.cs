using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MeetHut.Backend.Configurations;
using MeetHut.Backend.Middlewares;
using MeetHut.DataAccess;
using MeetHut.DataAccess.Entities;
using MeetHut.Services.Application;
using MeetHut.Services.Application.Interfaces;
using MeetHut.Services.Application.Mappers;
using MeetHut.Services.Configurations;
using MeetHut.Services.Meet;
using MeetHut.Services.Meet.Hubs;
using MeetHut.Services.Meet.Interfaces;
using MeetHut.Services.Meet.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
            services.Configure<JWTConfiguration>(Configuration.GetSection("Jwt"));
            services.Configure<GoogleConfiguration>(Configuration.GetSection("ExternalAuthentication:Google"));
            services.Configure<MicrosoftConfiguration>(Configuration.GetSection("ExternalAuthentication:Microsoft"));
            services.Configure<MigrationConfiguration>(Configuration.GetSection("Migration"));

            services.AddHttpClient();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .WithOrigins(Configuration.GetValue("ClientUrl", "http://localhost:4200"), "http://localhost:5000", "https://localhost:5001");
                    });
            });

            // Add services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IParameterService, ParameterService>();

            // Hosted services
            services.AddHostedService<RoomWorker>();

            // Add mappers
            var mapperConfig = new MapperConfiguration(conf => { conf.AddProfile<UserMapper>(); conf.AddProfile<RoomMapper>(); });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Add Database context
            string connectionString = Configuration.GetConnectionString(Configuration.GetValue<bool>("UseDesignTimeConnection") ? "DesignTimeConnection" : "DefaultConnection");
            services.AddDbContextPool<DatabaseContext>(options =>
                options.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                    builder => builder.MigrationsAssembly("MeetHut.DataAccess")));

            services
                .AddIdentity<User, Role>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            // Add controllers
            services.AddControllers();

            // Register auth
            services
                .AddAuthorization()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
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

            services.AddSignalR();

            // Register swagger display
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MeetHut.Backend", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT containing userid claim",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                var security =
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                },
                                UnresolvedReference = true
                            },
                            new List<string>()
                        }
                    };
                c.AddSecurityRequirement(security);
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
        /// <param name="roleManager">Role Manager</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<Role> roleManager)
        {
            IdentityDataInitalizer.SeedRoles(roleManager);

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MeetHut.Backend v1"));
            }

            app.UseWebSockets();

            app.UseServerExceptionHandler();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseCors("CorsPolicy");
            }

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MessageHub>("/Chat"); 
                endpoints.MapControllers();
            });

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