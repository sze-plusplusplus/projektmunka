using System.ComponentModel.DataAnnotations;

namespace MeetHut.Services.Application.Models
{
    /// <summary>
    /// Login Model
    /// </summary>
    public class LoginModel
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
        public string Password { get; set; }
    }
}