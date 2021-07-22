using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetHut.DataAccess.Entities.Meet
{
    /// <summary>
    /// Meet room entity
    /// </summary>
    public class Room : Entity
    {
        public string Name { get; set; }

        public string PublicId { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool Locked { get; set; } = false;

        public IList<RoomUser> RoomUsers { get; set; }
    }
}