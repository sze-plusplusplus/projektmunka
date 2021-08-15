using System;
using System.ComponentModel.DataAnnotations;

namespace MeetHut.Services.Application.Models
{
    /// <summary>
    /// User Model
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Unique user name
        /// </summary>
        [Required]
        public string UserName { get; set; }
        
        /// <summary>
        /// Email address
        /// </summary>
        [Required]
        public string Email { get; set; }
        
        /// <summary>
        /// Full name
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Last login date
        /// </summary>
        [Required]
        public DateTime LastLogin { get; set; }
    }
}