using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Models;
using System.Security.Claims;

namespace MeetHut.Services.Application.Interfaces
{
    /// <summary>
    /// Token Service
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Build access token
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Created token</returns>
        string BuildAccessToken(UserDTO user);

        /// <summary>
        /// Build refresh token
        /// </summary>
        /// <returns>Created token</returns>
        string BuildRefreshToken();

        /// <summary>
        /// Validate input token
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>True if it is valid</returns>
        bool ValidateToken(string token);

        /// <summary>
        /// Get claims from token
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>Claim principals</returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        /// <summary>
        /// Refresh tokens
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Refreshed tokens</returns>
        TokenDTO Refresh(TokenModel model);
    }
}
