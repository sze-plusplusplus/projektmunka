using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            try
            {
                var db = services.GetRequiredService<T>();
                db.Database.Migrate();
            }
            catch (Exception e)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(e, "An error occurred while migration the database.");
            }

            return webHost;
        }
    }
}