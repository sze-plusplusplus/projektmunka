using System.Linq;
using AutoMapper;
using MeetHut.DataAccess;
using MeetHut.DataAccess.Entities.Meet;
using Microsoft.Extensions.Configuration;
using MeetHut.Services.Meet.Interfaces;
using MeetHut.Services.Meet.DTOs;
using MeetHut.DataAccess.Entities;
using Livekit.Client;

namespace MeetHut.Services.Meet
{
    /// <inheritdoc cref="MeetHut.Services.Meet.Interfaces.IRoomService" />
    public class RoomService : MapperRepository<Room>, IRoomService
    {
        private readonly IMapper _mapper;

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
        public RoomService(DatabaseContext databaseContext, IMapper mapper, IConfiguration configuration) : base(databaseContext, mapper)
        {
            _mapper = mapper;
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
            return _mapper.Map<RoomDTO[]>(this.DatabaseContext.Rooms.Where(r => r.RoomUsers.Any(u => u.UserId == user.Id)).ToArray());
        }

        /// <inheritdoc />
        public RoomDTO GetByPublicId(string publicId)
        {
            return _mapper.Map<RoomDTO>(this.DatabaseContext.Rooms.Where(r => r.PublicId == publicId).First());
        }

        /// <inheritdoc />
        public RoomUser[] GetRoomUsers(int roomId)
        {
            return this.DatabaseContext.RoomUsers.Where(r => r.RoomId == roomId).ToArray();
        }
    }
}