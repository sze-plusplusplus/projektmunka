using System;
using System.ComponentModel.DataAnnotations;

namespace MeetHut.Services.Meet.Models
{
    /// <summary>
    /// Room Model
    /// </summary>
    public class RoomModel
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
        /// Owner Id
        /// </summary>
        [Required]
        public int OwnerId { get; set; }

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