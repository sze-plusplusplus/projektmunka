using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using MeetHut.Services.Meet.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MeetHut.Services.Meet
{
    /// <summary>
    /// Worker process for the RoomService
    /// </summary>
    public class RoomWorker : BackgroundService
    {
        private readonly IServiceProvider _sp;

        private readonly bool Logging;
        private readonly int TimerInterval;

        /// <summary>
        /// RoomWorker
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="configuration"></param>
        public RoomWorker(
            IServiceProvider sp, IConfiguration configuration)
        {
            _sp = sp;
            Logging = bool.Parse(configuration["Livekit:workerLogging"]);
            TimerInterval = int.Parse(configuration["Livekit:workerInterval"]);
        }

        /// <summary>
        /// Execute tasks for Rooms
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (Logging)
                    PingLivekit();
                await Task.Delay(TimerInterval * 1000, cancellationToken);
            }
        }

        /// <summary>
        /// List some data periodically of the Livekit instance
        /// </summary>
        private void PingLivekit()
        {
            using (var scope = _sp.CreateScope())
            {
                IRoomService rs = scope.ServiceProvider.GetRequiredService<IRoomService>();

                System.Console.WriteLine($"Livekit: {rs.GetStats()}");
            }
        }
    }
}