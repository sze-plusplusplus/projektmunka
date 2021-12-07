using AutoMapper;
using MeetHut.DataAccess.Entities.Meet;
using MeetHut.Services.Meet.DTOs;
using MeetHut.Services.Meet.Models;

namespace MeetHut.Services.Meet.Mappers
{
    /// <summary>
    /// Room Mapper
    /// </summary>
    public class RoomMapper : Profile
    {
        /// <summary>
        /// Init Room Mapper
        /// </summary>
        public RoomMapper()
        {
            CreateMap<Room, RoomDTO>();
            CreateMap<RoomModel, Room>();

            CreateMap<Room, RoomCalendarDTO>()
                .ForMember(dest => dest.IsLocked, opt => opt.MapFrom(src => src.Locked))
                .ForMember(dest => dest.IsOwner, opt => opt.MapFrom(src => false));
        }
    }
}