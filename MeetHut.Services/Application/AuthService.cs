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

                return new TokenDTO { AccessToken = _tokenService.BuildAccessToken(_userService.GetMapped(user.Id)), RefreshToken = refreshToken };
            }
            else
            {
                throw new ArgumentException("Invalid password");
            }
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
            var user = _userService.GetMappedByName(userName);

            if (user == null)
            {
                throw new ArgumentException("User does not exist");
            }

            _userService.UpdateAndSaveByModel(user.Id, new UserTokenRefreshModel { RefreshToken = null });
        }

        private static string HashPassword(string password, byte[] salt = null, bool needsOnlyHash = false)
        {
            if (salt == null || salt.Length != 16)
            {
                // generate a 128-bit salt using a secure PRNG
                salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8)
            );

            if (needsOnlyHash)
            {
                return hashed;
            }

            // password will be concatenated with salt using ':'
            return $"{hashed}:{Convert.ToBase64String(salt)}";
        }

        private static bool VerifyPassword(string hashedPasswordWithSalt, string passwordToCheck)
        {
            // retrieve both salt and password from 'hashedPasswordWithSalt'
            var passwordAndHash = hashedPasswordWithSalt.Split(':');
            if (passwordAndHash == null || passwordAndHash.Length != 2)
            {
                return false;
            }

            var salt = Convert.FromBase64String(passwordAndHash[1]);
            if (salt == null)
            {
                return false;
            }

            // hash the given password
            var hashOfpasswordToCheck = HashPassword(passwordToCheck, salt, true);
            // compare both hashes
            if (string.Compare(passwordAndHash[0], hashOfpasswordToCheck) == 0)
            {
                return true;
            }

            return false;
        }
    }
}