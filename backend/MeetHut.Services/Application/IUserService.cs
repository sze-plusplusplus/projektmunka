using MeetHut.DataAccess.Entities;
using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Models;

namespace MeetHut.Services.Application
{
    /// <summary>
    /// User Service
    /// </summary>
    public interface IUserService : IMapperRepository<User, UserDto, UserModel>
    {
        
    }
}