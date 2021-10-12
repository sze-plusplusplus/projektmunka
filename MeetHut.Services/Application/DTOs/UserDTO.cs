using System;

namespace MeetHut.Services.Application.DTOs
{
    /// <summary>
    /// User DTO object
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Last login date
        /// </summary>
        public DateTime LastLogin { get; set; }

        /// <summary>
        /// Access token refresh token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Expiry time of the Refresh token
        /// </summary>
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}