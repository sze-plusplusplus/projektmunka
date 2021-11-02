using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetHut.Services.Application.DTOs
{
    /// <summary>
    /// User DTO for token management
    /// </summary>
    public class UserTokenDTO
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
        /// Access token refresh token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Expiry time of the Refresh token
        /// </summary>
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
