using MeetHut.DataAccess.Entities;

namespace MeetHut.Services.Application.Interfaces
{
    /// <summary>
    /// User Service
    /// </summary>
    public interface IUserService : IMapperRepository<User>
    {
        /// <summary>
        /// Get User by name
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>User</returns>
        User GetByName(string userName);

        /// <summary>
        /// Get mapped User by name
        /// </summary>
        /// <typeparam name="T">Type of map</typeparam>
        /// <param name="userName">User name</param>
        /// <returns>T DTO</returns>
        T GetMappedByName<T>(string userName);

        /// <summary>
        /// Get User by email address
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>User</returns>
        User GetByEmail(string email);

        /// <summary>
        /// Get mapped User by Id
        /// </summary>
        /// <typeparam name="T">Type of map</typeparam>
        /// <param name="id">User id</param>
        /// <returns>User map</returns>
        T GetMappedById<T>(int id);

        /// <summary>
        /// Get user by refresh token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        User GetByRefreshToken(string token);

        /// <summary>
        /// Is user exist with given name or email
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="email">Email address</param>
        /// <returns>True if exists</returns>
        bool IsExist(string userName, string email);
    }
}