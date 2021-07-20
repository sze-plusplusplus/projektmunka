using System;
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
            CreateMap<User, UserDto>();
            CreateMap<UserModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => "Password hash"))
                .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Creation, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}