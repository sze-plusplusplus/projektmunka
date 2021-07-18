using MeetHut.DataAccess;
using MeetHut.DataAccess.Entities;
using MeetHut.DataAccess.Repositories;

namespace MeetHut.Services.Application
{
    /// <inheritdoc cref="MeetHut.Services.Application.IUserService" />
    public class UserService : Repository<User>, IUserService
    {
        /// <summary>
        /// Init User Service
        /// </summary>
        /// <param name="databaseContext">Database Context</param>
        public UserService(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}