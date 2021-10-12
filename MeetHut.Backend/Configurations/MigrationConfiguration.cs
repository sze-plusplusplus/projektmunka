using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetHut.Backend.Configurations
{
    /// <summary>
    /// Migration configuration
    /// </summary>
    public class MigrationConfiguration
    {
        /// <summary>
        /// Migrate on startup
        /// </summary>
        public bool OnStart { get; set; }
    }
}
