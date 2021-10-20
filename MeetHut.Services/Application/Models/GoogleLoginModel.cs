using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetHut.Services.Application.Models
{
    /// <summary>
    /// Google Login Model
    /// </summary>
    public class GoogleLoginModel
    {
        /// <summary>
        /// Id Token
        /// </summary>
        public string IdToken { get; set; }

        /// <summary>
        /// Provider
        /// </summary>
        public string Provider { get; set; }
    }
}
