using MeetHut.Backend.Middlewares;
using MeetHut.DataAccess;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

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
            CreateWebHost(args)
                .MigrateDatabase<DatabaseContext>()
                .Run();
        }

        private static IWebHost CreateWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();
        }
    }
}