using AutoMapper;
using MeetHut.DataAccess.Entities;
using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Models;

namespace MeetHut.Services.Application.Mappers
{
    /// <summary>
    /// User mapper
    /// </summary>
    public class UserMapper : Profile
    {
        /// <summary>
        /// Init User mapper
        /// </summary>
        public UserMapper()
        {
            CreateMap<User, UserDTO>();
            CreateMap<User, UserTokenDTO>();
            CreateMap<UserModel, User>();
            CreateMap<UserTokenRefreshModel, User>();
        }
    }
}