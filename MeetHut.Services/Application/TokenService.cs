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

namespace MeetHut.Services.Application
{
    /// <inheritdoc />
    public class TokenService : ITokenService
    {
        private readonly JWTConfiguration _jwtConfigurations;
        private readonly IUserService _userService;

        /// <summary>
        /// Init Token Service
        /// </summary>
        /// <param name="jwtOptions">JWT Configuration</param>
        /// <param name="userService">User Service</param>
        public TokenService(IOptions<JWTConfiguration> jwtOptions, IUserService userService)
        {
            _jwtConfigurations = jwtOptions.Value;
            _userService = userService;
        }

        /// <inheritdoc />
        public string BuildAccessToken(UserDTO user)
        {
            var key = _jwtConfigurations.Key;
            var issuer = _jwtConfigurations.Issuer;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);
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
            var tokenValidationParams = GetTokenValidationParameters();
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
            var tokenValidationParams = GetTokenValidationParameters();
            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        /// <inheritdoc />
        public TokenDTO Refresh(TokenModel model)
        {
            if (model == null)
            {
                throw new ArgumentException("Input model is invalid");
            }

            var principal = GetPrincipalFromExpiredToken(model.AccessToken);
            var user = _userService.GetMappedByName(principal.Identity.Name);

            if (user == null)
            {
                throw new ArgumentException("User does not exists");
            }
            else if (user.RefreshToken != model.RefreshToken)
            {
                throw new ArgumentException("Refresh token is invalid");
            }
            else if (user.RefreshTokenExpiryTime < DateTime.Now)
            {
                throw new ArgumentException("Refresh token is expired");
            }

            var accessToken = BuildAccessToken(user);
            var refreshToken = BuildRefreshToken();

            _userService.UpdateAndSave(user.Id, new UserTokenRefreshModel { RefreshToken = refreshToken });

            return new TokenDTO { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        private TokenValidationParameters GetTokenValidationParameters()
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
                IssuerSigningKey = mySecurityKey
            };
        }
    }
}
