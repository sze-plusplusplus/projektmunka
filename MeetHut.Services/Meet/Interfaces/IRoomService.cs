using MeetHut.DataAccess.Entities.Meet;
using MeetHut.Services.Meet.DTOs;
using MeetHut.Services.Meet.Models;

namespace MeetHut.Services.Meet.Interfaces
{
    /// <summary>
    /// Room service
    /// </summary>
    public interface IRoomService : IMapperRepository<Room, RoomDTO>
    {
        
    }
}