using System;

namespace MeetHut.Services.Meet.DTOs
{
    /// <summary>
    /// Room Calendar Event DTO
    /// </summary>
    public class RoomCalendarDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Room name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Public Id
        /// </summary>
        public string PublicId { get; set; }
        
        /// <summary>
        /// Start time
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// End time
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Room is locked
        /// </summary>
        public bool IsLocked { get; set; } = false;
        
        /// <summary>
        /// Current user is the owner
        /// </summary>
        public bool IsOwner { get; set; }
    }
}