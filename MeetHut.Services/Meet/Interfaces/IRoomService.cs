using System.Collections.Generic;
using MeetHut.DataAccess.Entities;
using MeetHut.DataAccess.Entities.Meet;
using MeetHut.DataAccess.Enums.Meet;
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
        /// Get the list of the users mapped to DTO class
        /// </summary>
        /// <param name="roomId">ID of the room</param>
        public RoomUserDTO[] GetRoomUsersMapped(int roomId);
        /// <summary>
        /// Delete a room from the Livekit instance (will close connections) and remove room from db
        /// </summary>
        /// <param name="roomId">ID of the room</param>
        public void DeleteRoomWithLivekit(int roomId);
        /// <summary>
        /// Add user to the room, will search the user based on the provided username/email
        /// </summary>
        /// <param name="roomId">ID of the room</param>
        /// <param name="usernameOrEmail">Username or email address</param>
        /// <param name="adderId">ID of the adding (current user)</param>
        /// <param name="role">Role of the user</param>
        public void AddToRoom(int roomId, string usernameOrEmail, int adderId, MeetRole role);
        /// <summary>
        /// Add a user to a room with given role
        /// </summary>
        /// <param name="roomId">ID of room</param>
        /// <param name="userId">ID of user</param>
        /// <param name="adderId">ID of adding user (current)</param>
        /// <param name="role">Role of user, default GUEST</param>
        public void AddToRoom(int roomId, int userId, int adderId, MeetRole role);
        /// <summary>
        /// Remove a user from a room
        /// </summary>
        /// <param name="roomId">ID of the room</param>
        /// <param name="userId">ID of the user</param>
        public void RemoveFromRoom(int roomId, int userId);
        
        /// <summary>
        /// Get Calendar event entity list for a user
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>List of room events</returns>
        List<RoomCalendarDTO> GetCalendar(string userName);
    }
}