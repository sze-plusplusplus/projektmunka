using MeetHut.Services.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MeetHut.Services.Application.DTOs;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using MeetHut.Services.Configurations;
using MeetHut.Services.Application.Models;
using Microsoft.AspNetCore.Identity;
using MeetHut.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetHut.Services.Application
{
    /// <inheritdoc />
    public class TokenService : ITokenService
    {
        private readonly JWTConfiguration _jwtConfigurations;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Init Token Service
        /// </summary>
        /// <param name="jwtOptions">JWT Configuration</param>
        /// <param name="userService">User Service</param>
        /// <param name="userManager">User Manager</param>
        public TokenService(IOptions<JWTConfiguration> jwtOptions, IUserService userService, UserManager<User> userManager)
        {
            _jwtConfigurations = jwtOptions.Value;
            _userService = userService;
            _userManager = userManager;
        }

        /// <inheritdoc />
        public string BuildAccessToken(UserTokenDTO user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigurations.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(_jwtConfigurations.Issuer, _jwtConfigurations.Issuer, claims, expires: DateTime.Now.AddMinutes(_jwtConfigurations.ExpirationInMinutes), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        /// <inheritdoc />
        public string BuildRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /// <inheritdoc />
        public bool ValidateToken(string token)
        {
            var tokenValidationParams = GetTokenValidationParameters(true);
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, tokenValidationParams, out _);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token cannot be null");
            }

            var tokenValidationParams = GetTokenValidationParameters(false);
            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        /// <inheritdoc />
        public async Task<TokenDTO> Refresh(TokenModel model)
        {
            if (model == null)
            {
                throw new ArgumentException("Input model is invalid");
            }

            var principal = GetPrincipalFromExpiredToken(model.AccessToken);

            if (principal.Identity == null || string.IsNullOrEmpty(principal.Identity.Name))
            {
                throw new ArgumentException("Invalid token data");
            }

            UserTokenDTO user = _userService.GetMappedByName<UserTokenDTO>(principal.Identity.Name);

            if (user == null)
            {
                throw new ArgumentException("User does not exists");
            }

            if (user.RefreshToken != model.RefreshToken)
            {
                throw new ArgumentException("Refresh token is invalid");
            }

            if (user.RefreshTokenExpiryTime < DateTime.Now)
            {
                throw new ArgumentException("Refresh token is expired");
            }

            var accessToken = BuildAccessToken(user, await _userManager.GetRolesAsync(_userManager.Users.FirstOrDefault(u => u.Id == user.Id)));
            var refreshToken = BuildRefreshToken();

            _userService.UpdateAndSaveByModel(user.Id, new UserTokenRefreshModel { RefreshToken = refreshToken });

            return new TokenDTO { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        private TokenValidationParameters GetTokenValidationParameters(bool validateLifetime = true)
        {
            var key = _jwtConfigurations.Key;
            var issuer = _jwtConfigurations.Issuer;
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);

            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidIssuer = issuer,
                IssuerSigningKey = mySecurityKey,
                ValidateLifetime = validateLifetime
            };
        }
    }
}
