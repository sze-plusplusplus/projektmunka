using AutoMapper;
using MeetHut.DataAccess;
using MeetHut.DataAccess.Entities;
using MeetHut.Services.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace MeetHut.Services.Application
{
    /// <inheritdoc cref="MeetHut.Services.Application.Interfaces.IUserService" />
    public class UserService : MapperRepository<User>, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        /// <summary>
        /// Init User Service
        /// </summary>
        /// <param name="userManager">User Manager</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="context">Database context</param>
        public UserService(UserManager<User> userManager, IMapper mapper, DatabaseContext context) : base(context, mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public User GetByEmail(string email)
        {
            return _userManager.Users.SingleOrDefault(user => user.Email == email);
        }

        /// <inheritdoc />
        public User GetByName(string userName)
        {
            return _userManager.Users.SingleOrDefault(user => user.UserName == userName);
        }

        /// <inheritdoc />
        public T GetMappedByName<T>(string userName)
        {
            return _mapper.Map<T>(GetByName(userName));
        }

        /// <inheritdoc />
        public T GetMappedById<T>(int id)
        {
            return _mapper.Map<T>(Get(id));
        }

        /// <inheritdoc />
        public bool IsExist(string userName, string email)
        {
            return _userManager.Users.Any(user => user.Email == email || user.UserName == userName);
        }

        /// <inheritdoc />
        public User GetByRefreshToken(string token)
        {
            return GetList(x => x.RefreshTokens.Any(t => t.Token == token)).FirstOrDefault();
        }
    }
}