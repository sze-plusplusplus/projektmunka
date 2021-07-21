using MeetHut.Services.Application;
using MeetHut.Services.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetHut.Backend.Controllers
{
    /// <summary>
    /// Auth Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthController
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
        public string Login([FromBody] LoginModel model)
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
    }
}