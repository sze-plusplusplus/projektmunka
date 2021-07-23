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
        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        /// <summary>
        /// Room Id
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Room
        /// </summary>
        public Room Room { get; set; }

        /// <summary>
        /// Meet Role
        /// </summary>
        public MeetRole Role { get; set; }

        /// <summary>
        /// Added time
        /// </summary>
        public DateTime Added { get; set; }

        /// <summary>
        /// Adder Id
        /// </summary>
        public int AdderId { get; set; }

        /// <summary>
        /// Adder
        /// </summary>
        [ForeignKey("AdderId")]
        public virtual User Adder { get; set; }
    }
}