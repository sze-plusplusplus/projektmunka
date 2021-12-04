using System;
using MeetHut.DataAccess.Enums.Meet;
using MeetHut.Services.Application.DTOs;

namespace MeetHut.Services.Meet.DTOs
{
    /// <summary>
    /// RoomUser DTO
    /// </summary>
    public class RoomUserDTO
    {
        /// <summary>
        /// UserId
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// User
        /// </summary>
        public UserDTO User { get; set; }
        /// <summary>
        /// RoomId
        /// </summary>
        public int RoomId { get; set; }
        /// <summary>
        /// Role
        /// </summary>
        public MeetRole Role { get; set; }
        /// <summary>
        /// Added
        /// </summary>
        public DateTime Added { get; set; }
    }
}