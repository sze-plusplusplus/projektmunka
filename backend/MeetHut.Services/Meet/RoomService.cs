using AutoMapper;
using MeetHut.DataAccess;
using MeetHut.DataAccess.Entities.Meet;
using MeetHut.Services.Meet.DTOs;
using MeetHut.Services.Meet.Models;

namespace MeetHut.Services.Meet
{
    /// <inheritdoc cref="MeetHut.Services.Meet.IRoomService" />
    public class RoomService : MapperRepository<Room, RoomDTO, RoomModel>, IRoomService
    {
        /// <summary>
        /// Init Room Service
        /// </summary>
        /// <param name="databaseContext">Database Context</param>
        /// <param name="mapper">Mapper</param>
        public RoomService(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper)
        {
        }
    }
}