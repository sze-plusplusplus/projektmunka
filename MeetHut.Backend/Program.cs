using MeetHut.DataAccess;
using MeetHut.Backend.Middlewares;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MeetHut.Backend
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main function of the Backend app
        /// </summary>
        /// <param name="args">Program arguments</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().MigrateDatabase<DatabaseContext>().Run();
        }

        /// <summary>
        /// CreateHostBuilder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}