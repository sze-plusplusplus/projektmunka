namespace MeetHut.Services.Application
{
    /// <summary>
    /// Authentication service
    /// </summary>
    public interface IAuthService
    {
        string Login();
        void Registration();
        void Logout();
    }
}