using System.Linq;
using System;
using System.Collections.Generic;
using AutoMapper;
using MeetHut.DataAccess;
using MeetHut.DataAccess.Entities.Meet;
using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Interfaces;
using MeetHut.Services.Meet.DTOs;
using Microsoft.Extensions.Configuration;
using MeetHut.Services.Meet.Interfaces;
using MeetHut.Services.Meet.DTOs;
using MeetHut.DataAccess.Entities;
using Livekit.Client;
using MeetHut.DataAccess.Enums.Meet;
using System;

namespace MeetHut.Services.Meet
{
    /// <inheritdoc cref="MeetHut.Services.Meet.Interfaces.IRoomService" />
    public class RoomService : MapperRepository<Room>, IRoomService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        private RoomClient client;
        private readonly string _lk_key;
        private readonly string _lk_secret;
        private readonly uint _lk_empty;
        private readonly uint _lk_max;

        /// <summary>
        /// Init Room Service
        /// </summary>
        /// <param name="databaseContext">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="userService">User Service</param>
        public RoomService(DatabaseContext databaseContext, IMapper mapper, IConfiguration configuration, IUserService userService) : base(databaseContext, mapper)
        {
            _mapper = mapper;
            _userService = userService;
            _lk_key = configuration["Livekit:key"];
            _lk_secret = configuration["Livekit:secret"];
            _lk_empty = uint.Parse(configuration["Livekit:emptyTimeout"]);
            _lk_max = uint.Parse(configuration["Livekit:maxParticipants"]);
            client = new RoomClient(configuration["Livekit:host"], _lk_key, _lk_secret);
        }

        /// <inheritdoc />
        public string GetStats()
        {
            var rooms = client.ListRooms();
            var users = rooms.Sum(r => r.NumParticipants);
            return $"Rooms:{rooms.Count()}\tParticipants:{users}";
        }

        /// <inheritdoc />
        public string ConnectionToken(User user, Room room)
        {
            client.CreateRoom(room.PublicId, _lk_empty, _lk_max);

            var at = new AccessToken(_lk_key, _lk_secret, new Grant()
            {
                identity = user.UserName,
                video = new VideoGrant()
                {
                    roomJoin = true,
                    room = room.PublicId,

                    canPublish = true,
                    canPublishData = true,
                    canSubscribe = true
                }
            }, user.UserName);

            return at.GetToken();
        }

        /// <inheritdoc />
        public void DeleteRoomWithLivekit(int roomId)
        {
            var room = Get(roomId);

            client.DeleteRoom(room.PublicId);

            DeleteByIdAndSave(roomId);
        }

        /// <inheritdoc />
        public RoomDTO[] GetAllOwnMapped(User user)
        {
            return withParticipantInfoMap(this.DatabaseContext.Rooms.Where(r => r.RoomUsers.Any(u => u.UserId == user.Id)).ToArray());
        }

        /// <inheritdoc />
        public RoomDTO GetByPublicId(string publicId)
        {
            return withParticipantInfoMap(this.DatabaseContext.Rooms.Where(r => r.PublicId == publicId).First());
        }

        /// <inheritdoc />
        public RoomUser[] GetRoomUsers(int roomId)
        {
            return this.DatabaseContext.RoomUsers.Where(r => r.RoomId == roomId).ToArray();
        }

        /// <inheritdoc />
        public RoomUserDTO[] GetRoomUsersMapped(int roomId)
        {
            return withParticipantJoinInfo(this.GetRoomUsers(roomId));
        }

        /// <inheritdoc />
        public void AddToRoom(int roomId, string usernameOrEmail, int adderId, MeetRole role = MeetRole.Guest)
        {
            usernameOrEmail = usernameOrEmail.ToUpperInvariant();
            var user = this.DatabaseContext.Users.Where(u => u.NormalizedUserName.Equals(usernameOrEmail) || u.NormalizedEmail.Equals(usernameOrEmail)).SingleOrDefault();

            if (user == null)
            {
                throw new System.ArgumentException("User cannot be found");
            }

            this.AddToRoom(roomId, user.Id, adderId, role);
        }


        /// <inheritdoc />
        public void AddToRoom(int roomId, int userId, int adderId, MeetRole role = MeetRole.Guest)
        {
            this.DatabaseContext.Add(new RoomUser()
            {
                AdderId = adderId,
                RoomId = roomId,
                UserId = userId,
                Role = role
            });
            this.DatabaseContext.SaveChanges();
        }

        /// <inheritdoc />
        public void RemoveFromRoom(int roomId, int userId)
        {
            var entity = this.DatabaseContext.RoomUsers.Find(roomId, userId);
            this.DatabaseContext.RoomUsers.Remove(entity);
            this.DatabaseContext.SaveChanges();
        }

        private RoomDTO withParticipantInfoMap(Room room)
        {
            var dto = _mapper.Map<RoomDTO>(room);

            dto.Participants = room.RoomUsers.Count();
            dto.OnlineParticipants = client.ListParticipants(room.PublicId).Count();

            return dto;
        }

        private RoomDTO[] withParticipantInfoMap(Room[] rooms)
        {
            return rooms.Select(r => withParticipantInfoMap(r)).ToArray();
        }

        private RoomUserDTO withParticipantJoinInfo(RoomUser user)
        {
            var dto = _mapper.Map<RoomUserDTO>(user);

            try
            {
                var participant = client.GetParticipant(user.Room.PublicId, user.User.UserName);
                if (participant != null)
                {
                    dto.IsOnline = true;
                    dto.JoinedAt = DateTimeOffset.FromUnixTimeSeconds(participant.JoinedAt).ToLocalTime().DateTime;
                }
            }
            catch { } // Not exists

            return dto;
        }

        private RoomUserDTO[] withParticipantJoinInfo(RoomUser[] users)
        {
            return users.Select(u => withParticipantJoinInfo(u)).ToArray();
        }
        
        /// <inheritdoc />
        public List<RoomCalendarDTO> GetCalendar(string userName)
        {
            var user = _userService.GetMappedByName<UserDTO>(userName);

            if (user is null)
            {
                throw new ArgumentException("User name cannot be empty");
            }
            return GetMappedList<RoomCalendarDTO>(room => room.StartTime != null 
                                                          && (room.OwnerId == user.Id 
                                                              || room.RoomUsers.Any(ru => ru.UserId == user.Id)))
                .ToList();
        }
    }
}