using System.ComponentModel.DataAnnotations;

namespace MeetHut.Services.Application.Models
{
    /// <summary>
    /// Registration Model
    /// </summary>
    public class RegistrationModel
    {
        /// <summary>
        /// User name
        /// </summary>
        [Required]
        [MaxLength(120)]
        public string UserName { get; set; }
        
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
        
        /// <summary>
        /// Email Address
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        /// <summary>
        /// Full name
        /// </summary>
        public string FullName { get; set; }

    }
}