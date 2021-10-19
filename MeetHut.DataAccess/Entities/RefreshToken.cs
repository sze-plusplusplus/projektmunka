using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetHut.DataAccess.Entities
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime Expires { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Revoked { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
