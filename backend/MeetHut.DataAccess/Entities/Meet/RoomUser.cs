using System;
using System.ComponentModel.DataAnnotations.Schema;
using MeetHut.DataAccess.Enums.Meet;

namespace MeetHut.DataAccess.Entities.Meet
{
    /// <summary>
    /// Meet room entity
    /// </summary>
    public class RoomUser
    {
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }

        public MeetRole Role { get; set; }

        public DateTime Added { get; set; }

        public int AdderId { get; set; }

        [ForeignKey("AdderId")]
        public virtual User Adder { get; set; }
    }
}