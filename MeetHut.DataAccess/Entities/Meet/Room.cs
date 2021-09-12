using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeetHut.DataAccess.Entities.Meet
{
    /// <summary>
    /// Meet room entity
    /// </summary>
    public class Room : Entity
    {
        /// <summary>
        /// Room name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Public Id
        /// </summary>
        [Required]
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
        public bool Locked { get; set; }

        /// <summary>
        /// Room Users
        /// </summary>
        public virtual ICollection<RoomUser> RoomUsers { get; set; }
        
        /// <summary>
        /// Owner
        /// </summary>
        public virtual User Owner { get; set; }
    }
}