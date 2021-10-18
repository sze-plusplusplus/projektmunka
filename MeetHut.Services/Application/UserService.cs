using AutoMapper;
using MeetHut.DataAccess;
using MeetHut.DataAccess.Entities;
using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Interfaces;
using System.Linq;

namespace MeetHut.Services.Application
{
    /// <inheritdoc cref="MeetHut.Services.Application.Interfaces.IUserService" />
    public class UserService : MapperRepository<Entity, UserDTO>, IUserService
    {
        /// <summary>
        /// Init User Service
        /// </summary>
        /// <param name="databaseContext">Database Context</param>
        /// <param name="mapper">Mapper</param>
        public UserService(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper)
        {

        }

        /// <inheritdoc />
        public User GetByEmail(string email)
        {
            return /*GetList(user => user.Email == email).FirstOrDefault();*/ null;
        }

        /// <inheritdoc />
        public User GetByName(string userName)
        {
            return /* GetList(user => user.UserName == userName).FirstOrDefault();*/ null;
        }

        /// <inheritdoc />
        public T GetMappedByName<T>(string userName)
        {
            return Mapper.Map<T>(GetByName(userName));
        }

        /// <inheritdoc />
        public T GetMappedById<T>(int id)
        {
            return Mapper.Map<T>(Get(id));
        }

        /// <inheritdoc />
        public bool IsExist(string userName, string email)
        {
            return /* GetList(user => user.Email == email || user.UserName == userName).FirstOrDefault() != null; */ false;
        }
    }
}