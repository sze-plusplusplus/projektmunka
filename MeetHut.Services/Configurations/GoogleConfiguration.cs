using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetHut.Services.Configurations
{
    /// <summary>
    /// Google config
    /// </summary>
    public class GoogleConfiguration
    {
        /// <summary>
        /// Client Id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Client Secret
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Login Disabled
        /// </summary>
        public bool LoginDisabled { get; set; }
    }
}
