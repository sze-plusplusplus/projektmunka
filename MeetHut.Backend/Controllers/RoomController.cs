using System.Collections.Generic;
using MeetHut.Services.Meet;
using MeetHut.Services.Meet.DTOs;
using MeetHut.Services.Meet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetHut.Backend.Controllers
{
    /// <summary>
    /// Room Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RoomController
    {
        private readonly IRoomService _roomService;

        /// <summary>
        /// Init User Controller
        /// </summary>
        /// <param name="roomService">Room Service</param>
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        
        /// <summary>
        /// Get all element 
        /// </summary>
        [HttpGet]
        public IEnumerable<RoomDTO> GetAll()
        {
            return _roomService.GetAllMapped();
        }

        /// <summary>
        /// Get room element
        /// </summary>
        /// <param name="id">Id</param>
        [HttpGet("{id:int}")]
        public RoomDTO Get(int id)
        {
            return _roomService.GetMapped(id);
        }

        /// <summary>
        /// Create room from model
        /// </summary>
        /// <param name="model">Model</param>
        [HttpPost]
        public int Create([FromBody] RoomModel model)
        {
            return _roomService.CreateAndSave(model);
        }
        
        /// <summary>
        /// Update room by model
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="model">Model</param>
        [HttpPut("{id:int}")]
        public void Update(int id, [FromBody] RoomModel model)
        {
            _roomService.UpdateAndSave(id, model);
        }
        
        /// <summary>
        /// Delete room by Id
        /// </summary>
        /// <param name="id">Id</param>
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            _roomService.DeleteByIdAndSave(id);
        }
    }
}