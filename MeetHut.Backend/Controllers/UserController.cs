using System.Collections.Generic;
using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Interfaces;
using MeetHut.Services.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetHut.Backend.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
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
        public IEnumerable<UserDTO> GetAll()
        {
            return _userService.GetAllMapped<UserDTO>();
        }

        /// <summary>
        /// Get user element
        /// </summary>
        /// <param name="id">Id</param>
        [HttpGet("{id:int}")]
        public UserDTO Get(int id)
        {
            return _userService.GetMapped<UserDTO>(id);
        }

        /// <summary>
        /// Create user from model
        /// </summary>
        /// <param name="model">Model</param>
        [HttpPost]
        public int Create([FromBody] UserModel model)
        {
            return _userService.CreateAndSaveByModel(model);
        }
        
        /// <summary>
        /// Update user by model
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="model">Model</param>
        [HttpPut("{id:int}")]
        public void Update(int id, [FromBody] UserModel model)
        {
            _userService.UpdateAndSaveByModel(id, model);
        }
        
        /// <summary>
        /// Delete user by Id
        /// </summary>
        /// <param name="id">Id</param>
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            _userService.DeleteByIdAndSave(id);
        }
    }
}