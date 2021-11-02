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
        public RefreshToken BuildRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.Now.AddDays(3),
                    Created = DateTime.Now
                };
            }
        }

        /// <inheritdoc />
        public async Task<TokenDTO> Refresh(TokenModel model)
        {
            if (model is null)
            {
                throw new ArgumentException("Input model is invalid");
            }

            var user = _userService.GetByRefreshToken(model.RefreshToken);

            if (user is null)
            {
                throw new ArgumentException("The refresh token is invalid");
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == model.RefreshToken);

            if (!refreshToken.IsActive)
            {
                throw new ArgumentException("The refresh token is expired");
            }

            refreshToken.Revoked = DateTime.Now;

            var newRefreshToken = BuildRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            _userService.UpdateAndSave(user);

            var accessToken = BuildAccessToken(_userService.GetMapped<UserTokenDTO>(user.Id), await _userManager.GetRolesAsync(_userManager.Users.FirstOrDefault(u => u.Id == user.Id)));

            return new TokenDTO { AccessToken = accessToken, RefreshToken = newRefreshToken.Token };
        }
    }
}
