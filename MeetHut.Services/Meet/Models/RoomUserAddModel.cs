namespace MeetHut.Services.Meet.Models
{
    /// <summary>
    /// Model for user adding input
    /// </summary>
    public class RoomUserAddModel
    {
        /// <summary>
        /// String containing a username or an email address
        /// </summary>
        public string UserNameOrEmail { get; set; }
    }
}