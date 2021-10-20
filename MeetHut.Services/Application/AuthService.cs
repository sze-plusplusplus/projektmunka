using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Google.Apis.Auth;
using MeetHut.DataAccess.Entities;
using MeetHut.DataAccess.Enums;
using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Interfaces;
using MeetHut.Services.Application.Models;
using MeetHut.Services.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MeetHut.Services.Application
{
    /// <inheritdoc />
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly GoogleConfiguration _googleConfiguration;
        private readonly MicrosoftConfiguration _microsoftConfiguration;
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Init Auth Service
        /// </summary>
        /// <param name="userService">User Service</param>
        /// <param name="tokenService">Token Service</param>
        /// <param name="userManager">User Manager</param>
        /// <param name="roleManager">Role Manager</param>
        /// <param name="googleOptions">Google options</param>
        public AuthService(IUserService userService, ITokenService tokenService, UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<GoogleConfiguration> googleOptions, IOptions<MicrosoftConfiguration> microsoftOptions, IHttpClientFactory clientFactory)
        {
            _userService = userService;
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
            _googleConfiguration = googleOptions.Value;
            _microsoftConfiguration = microsoftOptions.Value;
            _httpClientFactory = clientFactory;
        }

        /// <inheritdoc />
        public async Task<TokenDTO> Login(LoginModel model)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == model.UserName);

            if (user is null)
            {
                throw new ArgumentException("User not found");
            }

            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return await CreateTokensAndSave(user);
            }

            throw new ArgumentException("Incorrect username or password");
        }

        /// <inheritdoc />
        public async Task<TokenDTO> GoogleLogin(GoogleLoginModel model)
        {
            var payload = await VerifyGoogleToken(model);
            if (payload is null)
            {
                throw new ArgumentException("Google auth cannot be performed");
            }

            return await CreateExternalUser(model.Provider, payload.Subject, payload.Email, payload.Name);
        }

        /// <inheritdoc />
        public async Task<TokenDTO> MicrosoftLogin(MicrosoftLoginModel model)
        {


            var request = new HttpRequestMessage(HttpMethod.Get, $"{_microsoftConfiguration.GraphUrl}/oidc/userinfo");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", model.AuthToken);
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Dictionary<string, object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);

                string subject = (string)values["sub"];
                string email = (string)values["email"];
                string givenName = (string)values["given_name"];
                string familyName = (string)values["family_name"];

                return await CreateExternalUser(model.Provider, subject, email, $"{givenName} {familyName}");
            }

            throw new ArgumentException("Cannot connect to the Microsoft");
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
                LastLogin = DateTime.Now,
                RefreshTokens = new List<RefreshToken>()
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            await AddDefaultRoleByResult(result, user);
        }

        /// <inheritdoc />
        public void Logout(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("User is not logged in");
            }

            var user = _userService.GetByName(userName);

            if (user is null)
            {
                throw new ArgumentException("User does not exist");
            }

            foreach (var token in user.RefreshTokens)
            {
                if (token.IsActive)
                {
                    token.Revoked = DateTime.Now;
                }
            }

            _userService.UpdateAndSave(user);
        }

        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(GoogleLoginModel model)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _googleConfiguration.ClientId }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(model.IdToken, settings);
                return payload;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private async Task AddDefaultRoleByResult(IdentityResult result, User user)
        {
            if (!result.Succeeded)
            {
                throw new ArgumentException(result.Errors.FirstOrDefault()?.Description);
            }
            else
            {
                if (await _roleManager.RoleExistsAsync(UserRole.Student.ToString()))
                {
                    await _userManager.AddToRoleAsync(user, UserRole.Student.ToString());
                }
            }
        }

        private async Task<TokenDTO> CreateTokensAndSave(User user)
        {
            RefreshToken refreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
            if (refreshToken is null)
            {
                refreshToken = _tokenService.BuildRefreshToken();
                user.RefreshTokens.Add(refreshToken);
            }

            _userService.UpdateAndSave(user);

            return new TokenDTO { AccessToken = _tokenService.BuildAccessToken(_userService.GetMapped<UserTokenDTO>(user.Id), await _userManager.GetRolesAsync(user)), RefreshToken = refreshToken.Token, RefreshTokenExpiresIn = refreshToken.Expires };
        }

        private async Task<TokenDTO> CreateExternalUser(string provider, string subject, string email, string name)
        {
            var info = new UserLoginInfo(provider, subject, provider);

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(email);

                if (user is null)
                {
                    user = new User
                    {
                        UserName = email,
                        FullName = name,
                        Email = email,
                        LastLogin = DateTime.Now,
                        RefreshTokens = new List<RefreshToken>()
                    };

                    var result = await _userManager.CreateAsync(user);

                    await AddDefaultRoleByResult(result, user);
                }
                else
                {
                    await _userManager.AddLoginAsync(user, info);
                }
            }

            if (user is null)
            {
                throw new ArgumentException("Invalid External Authentication");
            }

            return await CreateTokensAndSave(user);
        }
    }
}