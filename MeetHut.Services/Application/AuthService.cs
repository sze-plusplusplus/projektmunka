using System;
using System.Security.Cryptography;
using MeetHut.DataAccess.Entities;
using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Interfaces;
using MeetHut.Services.Application.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MeetHut.Services.Application
{
    /// <inheritdoc />
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Init Auth Service
        /// </summary>
        /// <param name="userService">User Service</param>
        /// <param name="tokenService">Token Service</param>
        public AuthService(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        /// <inheritdoc />
        public TokenDTO Login(LoginModel model)
        {
            var user = _userService.GetByName(model.UserName);

            if (user == null)
            {
                throw new ArgumentException("Invalid username");
            }

            if (VerifyPassword(user.PasswordHash, model.Password))
            {
                var refreshToken = _tokenService.BuildRefreshToken();

                user.LastLogin = DateTime.Now;
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);

                _userService.UpdateAndSave(user);

                return new TokenDTO { AccessToken = _tokenService.BuildAccessToken(_userService.GetMappedById<UserTokenDTO>(user.Id)), RefreshToken = refreshToken };
            }

            throw new ArgumentException("Invalid password");
        }

        /// <inheritdoc />
        public void Registration(RegistrationModel model)
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
                PasswordHash = HashPassword(model.Password),
                LastLogin = DateTime.Now
            };

            _userService.CreateAndSave(user);
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

        private static string HashPassword(string password, byte[] salt = null, bool needsOnlyHash = false)
        {
            if (salt is not { Length: 16 })
            {
                // generate a 128-bit salt using a secure PRNG
                salt = new byte[128 / 8];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(salt);
            }

            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA256,
                10000,
                256 / 8)
            );

            // password will be concatenated with salt using ':'
            return needsOnlyHash ? hashed : $"{hashed}:{Convert.ToBase64String(salt)}";
        }

        private static bool VerifyPassword(string hashedPasswordWithSalt, string passwordToCheck)
        {
            // retrieve both salt and password from 'hashedPasswordWithSalt'
            var passwordAndHash = hashedPasswordWithSalt.Split(':');
            if (passwordAndHash is not { Length: 2 })
            {
                return false;
            }

            var salt = Convert.FromBase64String(passwordAndHash[1]);

            // hash the given password
            var hashOfPasswordToCheck = HashPassword(passwordToCheck, salt, true);
            // compare both hashes
            return string.CompareOrdinal(passwordAndHash[0], hashOfPasswordToCheck) == 0;
        }
    }
}