using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Interfaces;
using MeetHut.Services.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetHut.Backend.Controllers
{
    /// <summary>
    /// Auth Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Init Auth controller
        /// </summary>
        /// <param name="authService">Auth Service</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Token</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public TokenDTO Login([FromBody] LoginModel model)
        {
            return _authService.Login(model);
        }
        
        /// <summary>
        /// Registration
        /// </summary>
        /// <param name="model">Model</param>
        [HttpPost("registration")]
        [AllowAnonymous]
        public void Registration([FromBody] RegistrationModel model)
        {
            _authService.Registration(model);
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        public void Logout()
        {
            _authService.Logout(User.Identity.Name);
        }
    }
}