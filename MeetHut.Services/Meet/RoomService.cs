using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MeetHut.DataAccess;
using MeetHut.DataAccess.Entities.Meet;
using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Interfaces;
using MeetHut.Services.Meet.DTOs;
using Microsoft.Extensions.Configuration;
using MeetHut.Services.Meet.Interfaces;

namespace MeetHut.Services.Meet
{
    /// <inheritdoc cref="MeetHut.Services.Meet.Interfaces.IRoomService" />
    public class RoomService : MapperRepository<Room>, IRoomService
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Init Room Service
        /// </summary>
        /// <param name="databaseContext">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="userService">User Service</param>
        public RoomService(DatabaseContext databaseContext, IMapper mapper, IConfiguration configuration, IUserService userService) : base(databaseContext, mapper)
        {
            _userService = userService;
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
        
        /// <inheritdoc />
        public List<RoomCalendarDTO> GetCalendar(string userName)
        {
            var user = _userService.GetMappedByName<UserDTO>(userName);

            if (user is null)
            {
                throw new ArgumentException("User name cannot be empty");
            }
            return GetMappedList<RoomCalendarDTO>(room => room.StartTime != null 
                                                          && (room.OwnerId == user.Id 
                                                              || room.RoomUsers.Any(ru => ru.UserId == user.Id)))
                .ToList();
        }
    }
}