using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Models;

namespace MeetHut.Services.Application
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
        string Login(LoginModel model);
        
        /// <summary>
        /// Registration
        /// </summary>
        /// <param name="model">Model</param>
        void Registration(RegistrationModel model);

        /// <summary>
        /// Build token
        /// </summary>
        /// <param name="key">Secret key</param>
        /// <param name="issuer">Issuer</param>
        /// <param name="user">Logged in user</param>
        /// <returns>Built token</returns>
        string BuildToken(string key, string issuer, UserDTO user);
        
        /// <summary>
        /// Validate input token
        /// </summary>
        /// <param name="key">Secret key</param>
        /// <param name="issuer">Issuer</param>
        /// <param name="token">Token</param>
        /// <returns>True if it is valid</returns>
        bool ValidateToken(string key, string issuer, string token);
    }
}