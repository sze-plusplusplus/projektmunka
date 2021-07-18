using System.Collections.Generic;
using MeetHut.DataAccess.Entities;
using MeetHut.Services.Application;
using Microsoft.AspNetCore.Mvc;

namespace MeetHut.Backend.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Init User Controller
        /// </summary>
        /// <param name="userService">User Service</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        /// <summary>
        /// Get all element 
        /// </summary>
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _userService.GetAll();
        }
    }
}