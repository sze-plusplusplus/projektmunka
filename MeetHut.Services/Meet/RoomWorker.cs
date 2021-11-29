using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using MeetHut.Services.Meet.Interfaces;

namespace MeetHut.Services.Meet
{
    /// <summary>
    /// Worker process for the RoomService
    /// </summary>
    public class RoomWorker : BackgroundService
    {
        private readonly IServiceProvider _sp;

        /// <summary>
        /// RoomWorker
        /// </summary>
        /// <param name="sp"></param>
        public RoomWorker(
            IServiceProvider sp)
        {
            _sp = sp;
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
                PingLivekit();
                await Task.Delay(30000, cancellationToken);
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