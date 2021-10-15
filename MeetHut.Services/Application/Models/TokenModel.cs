namespace MeetHut.Services.Application.Models
{
    /// <summary>
    /// Token model
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// Access token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Refresh token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
