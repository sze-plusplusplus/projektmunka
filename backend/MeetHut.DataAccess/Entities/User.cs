using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using MeetHut.DataAccess.Enums;

namespace MeetHut.DataAccess.Entities
{
    /// <summary>
    /// User Entity
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// Unique user name
        /// </summary>
        [Required]
        [MaxLength(120)]
        public string UserName { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Login password
        /// </summary>
        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Last login date
        /// </summary>
        [Required]
        public DateTime LastLogin { get; set; }

        /// <summary>
        /// Role
        /// </summary>
        [Required]
        public UserRole Role { get; set; }

        //public IList<RoomUser> RoomUsers { get; set; }

        /// <inheritdoc />
        protected override Dictionary<string, string> GetKeyValuePairs()
        {
            return new()
            {
                {
                    "Id",
                    Id.ToString()
                },
                {
                    "LastUpdate",
                    Creation.ToString(CultureInfo.CurrentCulture)
                },
                {
                    "Creation",
                    Creation.ToString(CultureInfo.CurrentCulture)
                },
                {
                    "UserName",
                    UserName
                },
                {
                    "Email",
                    Email
                },
                {
                    "LastLogin",
                    LastLogin.ToString(CultureInfo.CurrentCulture)
                }
            };
        }
    }
}