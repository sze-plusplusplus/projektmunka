using AutoMapper;
using MeetHut.DataAccess;
using MeetHut.DataAccess.Entities;
using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Models;

namespace MeetHut.Services.Application
{
    /// <inheritdoc cref="MeetHut.Services.Application.IUserService" />
    public class UserService : MapperRepository<User, UserDTO, UserModel>, IUserService
    {
        /// <summary>
        /// Init User Service
        /// </summary>
        /// <param name="databaseContext">Database Context</param>
        /// <param name="mapper">Mapper</param>
        public UserService(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper)
        {
        }
    }
}