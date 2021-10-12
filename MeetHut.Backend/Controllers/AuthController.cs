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
    [AllowAnonymous]
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
        public TokenDTO Login([FromBody] LoginModel model)
        {
            return _authService.Login(model);
        }
        
        /// <summary>
        /// Registration
        /// </summary>
        /// <param name="model">Model</param>
        [HttpPost("registration")]
        public void Registration([FromBody] RegistrationModel model)
        {
            _authService.Registration(model);
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        [Authorize]
        public void Logout()
        {
            _authService.Logout(User.Identity.Name);
        }
    }
}