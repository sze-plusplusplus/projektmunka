using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using MeetHut.CommonTools.StringBuilder;

namespace MeetHut.DataAccess.Entities
{
    /// <summary>
    /// Base entity for database
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Entity Id
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Creation of the entity
        /// </summary>
        [Required]
        public DateTime Creation { get; set; }

        /// <summary>
        /// Last update of the entity
        /// </summary>
        [Required]
        public DateTime LastUpdate { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return StringBuilder.ToString(GetKeyValuePairs());
        }

        /// <summary>
        /// Generate key values pairs
        /// </summary>
        /// <returns>Map of values</returns>
        protected virtual Dictionary<string, string> GetKeyValuePairs()
        {
            return new()
            {
                {
                    "Id", Id.ToString()
                },
                {
                    "Creation", Creation.ToString(CultureInfo.CurrentCulture)
                },
                {
                    "Creation", Creation.ToString(CultureInfo.CurrentCulture)
                }
            };
        }
    }
}