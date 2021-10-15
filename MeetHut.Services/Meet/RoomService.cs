using AutoMapper;
using MeetHut.DataAccess;
using MeetHut.DataAccess.Entities.Meet;
using MeetHut.Services.Meet.DTOs;
using Microsoft.Extensions.Configuration;
using MeetHut.Services.Meet.Interfaces;

namespace MeetHut.Services.Meet
{
    /// <inheritdoc cref="MeetHut.Services.Meet.Interfaces.IRoomService" />
    public class RoomService : MapperRepository<Room, RoomDTO>, IRoomService
    {
        /// <summary>
        /// Init Room Service
        /// </summary>
        /// <param name="databaseContext">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="configuration">Configuration</param>
        public RoomService(DatabaseContext databaseContext, IMapper mapper, IConfiguration configuration) : base(databaseContext, mapper)
        {
            /*System.Console.WriteLine("Just testing Livekit connection...");
            var rs = new RoomClient(configuration["Livekit:host"], configuration["Livekit:key"], configuration["Livekit:secret"]);
            rs.CreateRoom("test1");
            rs.CreateRoom("test2");
            rs.CreateRoom("abcd2");
            System.Console.WriteLine("Rooms:");
            foreach (var current in rs.ListRooms())
            {
                var p = rs.ListParticipants(current.Name);
                System.Console.WriteLine("\t" + current.Name + ": " + p.Count);
            }*/
        }
    }
}