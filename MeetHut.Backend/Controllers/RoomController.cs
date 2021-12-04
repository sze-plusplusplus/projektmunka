using System;
using System.Linq;
using System.Collections.Generic;
using MeetHut.Services.Application.Interfaces;
using MeetHut.Services.Meet.DTOs;
using MeetHut.Services.Meet.Interfaces;
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
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IUserService _userService;

        /// <summary>
        /// Init User Controller
        /// </summary>
        /// <param name="roomService">Room Service</param>
        /// <param name="userService">User Service</param>
        public RoomController(IRoomService roomService, IUserService userService)
        {
            _roomService = roomService;
            _userService = userService;
        }

        /// <summary>
        /// Get all element (only admin, moderator)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public IEnumerable<RoomDTO> GetAll()
        {
            return _roomService.GetAllMapped<RoomDTO>();
        }

        /// <summary>
        /// Get all elements for current user 
        /// </summary>
        [HttpGet("own")]
        public IEnumerable<RoomDTO> GetAllOwn()
        {
            if (User.Identity == null)
            {
                return null;
            }

            var user = _userService.GetByName(User.Identity.Name);
            if (user is null)
            {
                throw new ArgumentException("Logged in user does not exist");
            }

            return _roomService.GetAllOwnMapped(user);
        }

        /// <summary>
        /// Get room element
        /// </summary>
        /// <param name="id">Id</param>
        [HttpGet("{id:int}")]
        public RoomDTO Get(int id)
        {
            checkUserHasPermission(id);

            return _roomService.GetMapped<RoomDTO>(id);
        }

        /// <summary>
        /// Get room element by publicId
        /// </summary>
        /// <param name="strId">strId</param>
        [HttpGet("publicId/{strId}")]
        public RoomDTO GetByName(string strId)
        {
            var room = _roomService.GetByPublicId(strId);
            checkUserHasPermission(0, room);

            return room;
        }

        /// <summary>
        /// Create room from model
        /// </summary>
        /// <param name="model">Model</param>
        [HttpPost]
        public int Create([FromBody] RoomModel model)
        {

            if (User.Identity == null)
            {
                return 0;
            }

            var user = _userService.GetByName(User.Identity.Name);
            if (user is null)
            {
                throw new ArgumentException("Logged in user does not exist");
            }

            var created = _roomService.CreateAndSaveByModel(setupModel(model));
            _roomService.AddToRoom(created, user.Id, user.Id, DataAccess.Enums.Meet.MeetRole.Lecturer);

            return created;
        }

        /// <summary>
        /// Update room by model
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="model">Model</param>
        [HttpPut("{id:int}")]
        public void Update(int id, [FromBody] RoomModel model)
        {
            checkUserHasPermission(id, null, true);

            _roomService.UpdateAndSaveByModel(id, setupModel(model));
        }

        /// <summary>
        /// Delete room by Id
        /// </summary>
        /// <param name="id">Id</param>
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            checkUserHasPermission(id, null, true);

            _roomService.DeleteRoomWithLivekit(id);
        }

        /// <summary>
        /// Get a livekit token to be able to connect to a room
        /// Room will be created (even if its not necessary)
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id:int}/open")]
        public OpenDTO Open(int id)
        {
            checkUserHasPermission(id);

            if (User.Identity == null)
            {
                return null;
            }

            var user = _userService.GetByName(User.Identity.Name);
            if (user is null)
            {
                throw new ArgumentException("Logged in user does not exist");
            }

            var room = _roomService.Get(id);

            return new OpenDTO()
            {
                RoomId = id,
                Token = _roomService.ConnectionToken(user, room)
            };
        }

        /// <summary>
        /// Get the list of the user entities connected to a room
        /// </summary>
        /// <param name="id">Room id</param>
        [HttpGet("{id:int}/users")]
        public RoomUserDTO[] GetRoomUsers(int id)
        {
            checkUserHasPermission(id);

            return _roomService.GetRoomUsersMapped(id);
        }

        /// <summary>
        /// Add a user to a room based on the username or email, should match exactly (case insensitive)
        /// </summary>
        /// <param name="roomId">Room id</param>
        /// <param name="model">Model containing the input string</param>
        [HttpPut("{roomId:int}/users")]
        public RoomUserDTO[] AddUserToRoom(int roomId, [FromBody] RoomUserAddModel model)
        {
            checkUserHasPermission(roomId, null, true);
            var user = _userService.GetByName(User.Identity.Name);
            if (user is null)
            {
                throw new ArgumentException("Logged in user does not exist");
            }

            _roomService.AddToRoom(roomId, model.UserNameOrEmail, user.Id, DataAccess.Enums.Meet.MeetRole.Student);

            return GetRoomUsers(roomId);
        }

        /// <summary>
        /// Remove the given user from the room
        /// </summary>
        /// <param name="roomId">Room id</param>
        /// <param name="userId">User id</param>
        [HttpDelete("{roomId:int}/users/{userId:int}")]
        public RoomUserDTO[] RemoveUserFromRoom(int roomId, int userId)
        {
            checkUserHasPermission(roomId, null, true);

            _roomService.RemoveFromRoom(roomId, userId);

            return GetRoomUsers(roomId);
        }

        // If Admin/Moderator, ok
        // if HighPriviledge, owner is required
        // else Roomuser record should exist
        private void checkUserHasPermission(int id, RoomDTO roomDTO = null, bool highPriviledge = false)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Moderator") || User.Identity == null)
            {
                return;
            }

            var user = _userService.GetByName(User.Identity.Name);
            if (user is null)
            {
                throw new ArgumentException("Logged in user does not exist");
            }

            var exists = false;
            if (id > 0 && roomDTO == null)
            {
                var room = _roomService.Get(id);

                if (highPriviledge && room.OwnerId != user.Id)
                {
                    throw new ArgumentException("Only room creator can execute this action");
                }
                exists = room.RoomUsers.Any(u => u.UserId == user.Id);
            }
            else
            {
                if (highPriviledge && roomDTO.OwnerId != user.Id)
                {
                    throw new ArgumentException("Only room creator can execute this action");
                }
                exists = _roomService.GetRoomUsers(roomDTO.Id).Any(u => u.UserId == user.Id);
            }

            if (!exists)
            {
                throw new ArgumentException("No permission to get this room");
            }
        }

        // Set the owner
        // fix the name whitespace
        // create a seo-friendly version of the name, starting with the username
        RoomModel setupModel(RoomModel model)
        {
            if (User.Identity == null)
            {
                return null;
            }

            var user = _userService.GetByName(User.Identity.Name);
            if (user is null)
            {
                throw new ArgumentException("Logged in user does not exist");
            }

            model.OwnerId = user.Id;
            model.Name = model.Name.Trim();
            model.PublicId = MeetHut.CommonTools.Utils.ToSeoFriendly.String(user.NormalizedUserName + "-" + model.Name);

            return model;
        }
    }
}