using System;
using MeetHut.Backend.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MeetHut.Backend.Middlewares
{

    /// <summary>
    /// Database Migration
    /// </summary>
    public static class DatabaseMigration
    {
        /// <summary>
        /// Migrate database
        /// </summary>
        /// <param name="webHost">Web host</param>
        /// <typeparam name="T">Type of database context</typeparam>
        /// <returns></returns>
        public static IHost MigrateDatabase<T>(this IHost webHost) where T : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var logger = services.GetRequiredService<ILogger<Program>>();

                var conf = services.GetRequiredService<IOptions<MigrationConfiguration>>().Value;

                if (!conf.OnStart)
                {
                    logger.LogInformation("Migration skipped...");
                    return webHost;
                }
                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<T>();
                    db.Database.Migrate();

                    logger.LogInformation("Database migrated...");
                }
                catch (Exception e)
                {
                    logger.LogError(e, "An error occurred while migration the database.");
                }
            }

            return webHost;
        }
    }
}