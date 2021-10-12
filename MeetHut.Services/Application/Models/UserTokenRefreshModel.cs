using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetHut.Services.Application.Models
{
    /// <summary>
    /// User token refresh update model
    /// </summary>
    public class UserTokenRefreshModel
    {
        /// <summary>
        /// Access token refresh token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
