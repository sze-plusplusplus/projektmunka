using MeetHut.DataAccess.Entities;
using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Models;

namespace MeetHut.Services.Application
{
    /// <summary>
    /// User Service
    /// </summary>
    public interface IUserService : IMapperRepository<User, UserDTO, UserModel>
    {
        /// <summary>
        /// Get User by name
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>User</returns>
        User GetByName(string userName);

        /// <summary>
        /// Get User by email address
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>User</returns>
        User GetByEmail(string email);

        /// <summary>
        /// Is user exist with given name or email
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="email">Email address</param>
        /// <returns>True if exists</returns>
        bool IsExist(string userName, string email);
    }
}