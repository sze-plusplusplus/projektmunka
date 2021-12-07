using System.Collections.Generic;
using MeetHut.DataAccess.Entities.Meet;
using MeetHut.Services.Meet.DTOs;

namespace MeetHut.Services.Meet.Interfaces
{
    /// <summary>
    /// Room service
    /// </summary>
    public interface IRoomService : IMapperRepository<Room>
    {
        /// <summary>
        /// Get Calendar event entity list for a user
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>List of room events</returns>
        List<RoomCalendarDTO> GetCalendar(string userName);
    }
}