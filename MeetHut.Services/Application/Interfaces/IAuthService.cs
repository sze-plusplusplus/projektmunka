using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Models;
using System.Threading.Tasks;

namespace MeetHut.Services.Application.Interfaces
{
    /// <summary>
    /// Authentication service
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Token</returns>
        Task<TokenDTO> Login(LoginModel model);
        
        /// <summary>
        /// Registration
        /// </summary>
        /// <param name="model">Model</param>
        Task Registration(RegistrationModel model);

        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="userName">User name</param>
        void Logout(string userName);
    }
}