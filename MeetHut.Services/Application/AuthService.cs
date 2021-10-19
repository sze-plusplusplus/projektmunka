using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MeetHut.DataAccess.Entities;
using MeetHut.DataAccess.Enums;
using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Interfaces;
using MeetHut.Services.Application.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;

namespace MeetHut.Services.Application
{
    /// <inheritdoc />
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        /// <summary>
        /// Init Auth Service
        /// </summary>
        /// <param name="userService">User Service</param>
        /// <param name="tokenService">Token Service</param>
        /// <param name="userManager">User Manager</param>
        /// <param name="roleManager">Role Manager</param>
        public AuthService(IUserService userService, ITokenService tokenService, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userService = userService;
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <inheritdoc />
        public async Task<TokenDTO> Login(LoginModel model)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == model.UserName);

            if (user is null)
            {
                throw new ArgumentException("User not found");
            }

            var signingResult = await _userManager.CheckPasswordAsync(user, model.Password);

            if (signingResult)
            {
                var refreshToken = _tokenService.BuildRefreshToken();

                user.LastLogin = DateTime.Now;
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);

                _userService.UpdateAndSave(user);

                return new TokenDTO { AccessToken = _tokenService.BuildAccessToken(_userService.GetMapped<UserTokenDTO>(user.Id), await _userManager.GetRolesAsync(user)), RefreshToken = refreshToken };
            }

            throw new ArgumentException("Incorrect username or password");
        }

        /// <inheritdoc />
        public async Task Registration(RegistrationModel model)
        {
            if (_userService.IsExist(model.UserName, model.Email))
            {
                throw new ArgumentException("User already created");
            }

            var user = new User
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
                LastLogin = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new ArgumentException(result.Errors.FirstOrDefault()?.ToString());
            } 
            else
            {
                if (await _roleManager.RoleExistsAsync(UserRole.Student.ToString()))
                {
                    await _userManager.AddToRoleAsync(user, UserRole.Student.ToString());
                }
            }
        }

        /// <inheritdoc />
        public void Logout(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("User is not logged in");
            }

            var user = _userService.GetMappedByName<UserTokenDTO>(userName);

            if (user == null)
            {
                throw new ArgumentException("User does not exist");
            }

            _userService.UpdateAndSaveByModel(user.Id, new UserTokenRefreshModel { RefreshToken = null });
        }
    }
}