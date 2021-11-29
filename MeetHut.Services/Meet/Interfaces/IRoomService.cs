using MeetHut.DataAccess.Entities;
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
        /// Get the number of rooms and participants active on the Livekit instance
        /// </summary>
        public string GetStats();
        /// <summary>
        /// Create a connection token tied to the given user and room
        /// </summary>
        /// <param name="user">Username will be used</param>
        /// <param name="room">PublicId is used</param>
        /// <returns>JWT token for a room</returns>
        public string ConnectionToken(User user, Room room);
        /// <summary>
        /// Get all rooms for a user (where RoomUser connection exists)
        /// </summary>
        /// <param name="user">User</param>
        public RoomDTO[] GetAllOwnMapped(User user);
        /// <summary>
        /// Get a room by the PublicId
        /// </summary>
        /// <param name="publicId">PublicId string</param>
        public RoomDTO GetByPublicId(string publicId);
        /// <summary>
        /// Get the users registered to a room (from db)
        /// </summary>
        /// <param name="roomId">ID of the room</param>
        public RoomUser[] GetRoomUsers(int roomId);
        /// <summary>
        /// Delete a room from the Livekit instance (will close connections) and remove room from db
        /// </summary>
        /// <param name="roomId">ID of the room</param>
        public void DeleteRoomWithLivekit(int roomId);
    }
}