using MeetHut.DataAccess.Entities;
using MeetHut.DataAccess.Repositories;

namespace MeetHut.Services.Application
{
    /// <summary>
    /// User Service
    /// </summary>
    public interface IUserService : IRepository<User>
    {
        
    }
}