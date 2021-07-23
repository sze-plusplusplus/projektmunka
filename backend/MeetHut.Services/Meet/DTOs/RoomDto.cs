using System;
using MeetHut.Services.Application.DTOs;

namespace MeetHut.Services.Meet.DTOs
{
    /// <summary>
    /// Room DTO
    /// </summary>
    public class RoomDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Creation
        /// </summary>
        public DateTime Creation { get; set; }

        /// <summary>
        /// Last update
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Room name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Public Id
        /// </summary>
        public string PublicId { get; set; }

        /// <summary>
        /// Owner Id
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Owner
        /// </summary>
        public UserDto Owner { get; set; }

        /// <summary>
        /// Start time
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// End time
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Room is locked
        /// </summary>
        public bool Locked { get; set; } = false;
    }
}