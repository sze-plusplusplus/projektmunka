using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MeetHut.Backend
{
    /// <summary>
    /// Program
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main function of the Backend app
        /// </summary>
        /// <param name="args">Program arguments</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create builder
        /// </summary>
        /// <param name="args">Program arguments</param>
        /// <returns>Host builder instance</returns>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}