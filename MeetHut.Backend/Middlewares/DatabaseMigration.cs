using System;
using MeetHut.Backend.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        public static IWebHost MigrateDatabase<T>(this IWebHost webHost) where T : DbContext
        {
            using var scope = webHost.Services.CreateScope();

            var services = scope.ServiceProvider;

            var logger = services.GetRequiredService<ILogger<Program>>();

            try
            {
                var conf = services.GetRequiredService<IOptions<MigrationConfiguration>>().Value;

                if (conf.OnStart)
                {
                    try
                    {
                        var db = services.GetRequiredService<T>();
                        db.Database.Migrate();
                        logger.LogInformation("Database migrated...");
                    }
                    catch (Exception e)
                    {
                        logger.LogError(e, "An error occurred while migration the database.");
                    }
                }
                else
                {
                    logger.LogInformation("Migration skipped...");
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred during the configuration getting.");
            }

            return webHost;
        }
    }
}