using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using MeetHut.DataAccess.Entities.Meet;
using MeetHut.DataAccess.Enums;
using Microsoft.AspNetCore.Identity;

namespace MeetHut.DataAccess.Entities
{
    /// <summary>
    /// User Entity
    /// </summary>
    public class User : IdentityUser<int>, IEntity
    {

        /// <summary>
        /// Full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Last login date
        /// </summary>
        [Required]
        public DateTime LastLogin { get; set; }
        
        /// <summary>
        /// Room Users
        /// </summary>
        public virtual ICollection<RoomUser> RoomUsers { get; set; }
        
        /// <summary>
        /// Owned Rooms
        /// </summary>
        public virtual ICollection<Room> OwnedRooms { get; set; }
        
        /// <summary>
        /// Added room users
        /// </summary>
        public virtual ICollection<RoomUser> AddedRoomUsers { get; set; }

        /// <summary>
        /// Refresh tokens
        /// </summary>
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

        public Dictionary<string, string> GetKeyValuePairs()
        {
            return new()
            {
                {
                    "Id",
                    Id.ToString()
                },
                {
                    "FullName",
                    FullName.ToString(CultureInfo.CurrentCulture)
                },
                {
                    "LastLogin",
                    LastLogin.ToString(CultureInfo.CurrentCulture)
                }
            };
        }
    }
}